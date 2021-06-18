using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moview_app.Models;

namespace Moview_app.Data
{
    public class Moview_appContext : DbContext
    {
        public Moview_appContext (DbContextOptions<Moview_appContext> options)
            : base(options)
        {
        }
        
        public DbSet<Moview_app.Models.Account> Account { get; set; }

        public DbSet<Moview_app.Models.Movie> Movie { get; set; }

        public DbSet<Moview_app.Models.Assessment> Assessment { get; set; }
    }
}
