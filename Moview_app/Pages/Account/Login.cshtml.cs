using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moview_app.Data;

namespace Moview_app.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly Moview_appContext _context;
        
        public LoginModel(Data.Moview_appContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AccountModel Account { get; set; }

        public class AccountModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Pass { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "必須の項目があります。";
                return Page();
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
            return LocalRedirect(returnUrl ?? Url.Content("~/Index" +""));
        }
    }
}
