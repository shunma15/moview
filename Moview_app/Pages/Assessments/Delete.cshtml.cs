using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moview_app.Data;
using Moview_app.Models;

namespace Moview_app.Pages.Assessments
{
    public class DeleteModel : PageModel
    {
        private readonly Moview_app.Data.Moview_appContext _context;

        public DeleteModel(Moview_app.Data.Moview_appContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assessment Assessment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assessment = await _context.Assessment.FirstOrDefaultAsync(m => m.ID == id);

            if (Assessment == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assessment = await _context.Assessment.FindAsync(id);

            if (Assessment != null)
            {
                _context.Assessment.Remove(Assessment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
