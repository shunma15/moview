using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moview_app.Models
{
    public class Assessment
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Posting { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
