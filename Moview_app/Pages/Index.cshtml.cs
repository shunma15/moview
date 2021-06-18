using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moview_app.Data;
using Microsoft.EntityFrameworkCore;
using Moview_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Moview_app.Pages
{
    public class HomepageModel : PageModel
    {
        private readonly Moview_appContext _context;
        
        public HomepageModel(Moview_appContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } 
        public IList<Models.Account> Account { get; set; }
        public IList<Assessment> Assessment { get; set; }

        public string Message { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public int ID { get; set; }


        public int Average(int id)
        {
           int averageVal = 0;
 
                foreach(var value in Assessment)
                {
                    if(id == value.MovieID)
                    {
                        int[] values;
                        int nums = 0;

                        values = _context.Assessment.Where(x => x.MovieID == id)
                            .Select(x => x.Value).ToArray();

                        for(int i = 0; i < values.Length; i++)
                        {
                            nums += values[i];
                        }
                        averageVal = nums / values.Length;
                    }
                    
                }
            
            return averageVal;
        }


        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
            Assessment = await _context.Assessment.ToListAsync();
            
            if (User.Identity.IsAuthenticated)
            {
                Name = _context.Account.FirstOrDefault(x =>
                       x.Username == User.Identity.Name).Username;
                Gender = _context.Account.FirstOrDefault(x =>
                       x.Username == User.Identity.Name).Gender;
                Age = _context.Account.FirstOrDefault(x =>
                      x.Username == User.Identity.Name).Age;
                ID = _context.Account.FirstOrDefault(x =>
                      x.Username == User.Identity.Name).ID;
            }
            else
            {
                Message = "ログインまたはアカウントの作成を行ってください。";
            }

        }

        public async Task OnPostAsync(string search)
        {

            if (search != null)
            {

                Movie = await _context.Movie.Where(m => m.Title.Contains(search) ||
                                m.Actor.Contains(search) || m.Director.Contains(search) ||
                                m.Genre.Contains(search)).ToListAsync();
                Assessment = await _context.Assessment.ToListAsync();

                if (User.Identity.IsAuthenticated)
                {

                    Name = _context.Account.FirstOrDefault(x =>
                           x.Username == User.Identity.Name).Username;
                    Gender = _context.Account.FirstOrDefault(x =>
                           x.Username == User.Identity.Name).Gender;
                    Age = _context.Account.FirstOrDefault(x =>
                          x.Username == User.Identity.Name).Age;
                    ID = _context.Account.FirstOrDefault(x =>
                          x.Username == User.Identity.Name).ID;
                }
                else
                {
                    Message = "ログインまたはアカウントの作成を行ってください。";
                }
            }
            else
            {
                await OnGetAsync();
            }        
        }
    }
}
