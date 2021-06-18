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

namespace Moview_app.Pages
{
   
    public class AddmovieModel : PageModel
    {

        private readonly Moview_app.Data.Moview_appContext _context;

        public AddmovieModel(Moview_app.Data.Moview_appContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }
  

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
