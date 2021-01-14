using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Printgate.Model
{
    public class TableReservationCustomF
    {
        public string CUSTOMF_NAME { get; set; }
        public string CUSTOMF_LNAME { get; set; }
        public string CUSTOMF_EMAIL { get; set; }
        public string Telefon { get; set; }
    }

    public class TableReservation
    {
        public long id { get; set; }
        public string checkin_ts { get; set; }
        public string purchaser_nominative { get; set; }
        public string purchaser_phone { get; set; }
        public string purchaser_mail { get; set; }
        public string status { get; set; }
        public int need_notif { get; set; }
        public string Name => purchaser_nominative;

        public string Phone => purchaser_phone;

        public string Date => _gate.GetDataTimeFromTimeStamp(double.Parse(checkin_ts));

        public string Email => purchaser_mail;

        public string Status => status;

        public string Print => need_notif == 2 ? "Yes" : "No";

        public string custom_f { get; set; }

        public TableReservationCustomF CustomF { get; set; }

        private Gate _gate;
        public TableReservation()
        {
            _gate = new Gate();
        }
    }
}
