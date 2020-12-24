using System;
using System.Collections.Generic;
using System.Text;

namespace Printgate.Model
{
    public class TableReservation
    {
        public long id { get; set; }
        public string date { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string print { get; set; }

        public TableReservation()
        {

        }
        public TableReservation(long id, string date, string name, string email, string status, string print)
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
