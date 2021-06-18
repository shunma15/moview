using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moview_app.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Pass { get; set; }
        public bool RememberMe { get; set; }
    }
}
