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
using Microsoft.AspNetCore.Authorization;

namespace Moview_app.Pages
{
    public class AssessmentModel : PageModel
    {
        private readonly Moview_app.Data.Moview_appContext _context;

        public AssessmentModel(Moview_app.Data.Moview_appContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Movie Movie { get; set; }
        [BindProperty]
        public Assessment Assessment { get; set; }
        [BindProperty]
        public Models.Account Account { get; set; }

        public string Message { get; set; }
        public int ID { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Message = "レビューをするためにはログインをして下さい。";
            }
            else
            {
                ID = _context.Account.FirstOrDefault(x => x.Username == User.Identity.Name).ID;
                Account = _context.Account.FirstOrDefault(x =>
                             x.ID == ID);
            }

            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);
            
            
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {

            Assessment.MovieID = Movie.ID;
            Assessment.AccountID = Account.ID;
            Assessment.Username = Account.Username;
            Assessment.Date = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _context.Assessment.Add(Assessment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }

        
    }
}
