using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moview_app.Data;
using Moview_app.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Moview_app.Pages
{
    public class NewregModel : PageModel
    {
        private readonly Moview_app.Data.Moview_appContext _context;

        public NewregModel(Moview_app.Data.Moview_appContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Account Account { get; set; }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Account.Add(Account);
            await _context.SaveChangesAsync();

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
    }
}
