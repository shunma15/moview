using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moview_app.Data;
using Moview_app.Models;

namespace Moview_app.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Moview_app.Data.Moview_appContext _context;

        public IndexModel(Moview_app.Data.Moview_appContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
