using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printgate.Model
{
    public class TakeAwayReservation
    {
        public long id { get; set; }
        public string date { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string print { get; set; }

        public TakeAwayReservation()
        {

        }

        public TakeAwayReservation(long id, string date, string name, string email, string status, string print)
        {
            this.id = id;
            this.name = name;
            this.date = date;
            this.email = email;
            this.status = status;
            this.print = print;
        }
    }
}
