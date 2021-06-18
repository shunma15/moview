using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moview_app.Data;
using Moview_app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Moview_app.Pages.Account
{
    public class EditAccountModel : PageModel
    {
        private readonly Moview_app.Data.Moview_appContext _context;

        public EditAccountModel(Moview_app.Data.Moview_appContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Account Account { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int id = _context.Account.FirstOrDefault(m =>
                      m.Username == User.Identity.Name).ID;

            Account = await _context.Account.FirstOrDefaultAsync(m => m.ID == id);

            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            bool isValid = _context.Account.SingleOrDefault(x =>
            x.Username == Account.Username && x.Pass == Account.Pass) != null;

            if (!isValid)
            {
                ErrorMessage = "ユーザー名またはパスワードに誤りがあります。";
                return Page();
            }
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, Account.Username),
                new Claim(ClaimTypes.Name, Account.Username)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = Account.RememberMe
                });

            return LocalRedirect(returnUrl ?? Url.Content("~/Index"));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.ID == id);
        }
    }
}
