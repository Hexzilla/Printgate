using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Printgate.Model
{
    public class TakeAwayReservationCustomF
    {
        public string CUSTOMF_TKNAME { get; set; }
        public string CUSTOMF_TKEMAIL { get; set; }
        public string CUSTOMF_TKPHONE { get; set; }
        public string CUSTOMF_TKADDRESS { get; set; }
        public string CUSTOMF_TKZIP { get; set; }
        public string CUSTOMF_TKNOTE { get; set; }
    }
    public class TakeAwayReservation
    {
        public long id { get; set; }
        public string checkin_ts { get; set; }
        public string purchaser_nominative { get; set; }
        public string purchaser_phone { get; set; }
        public string purchaser_mail { get; set; }
        public string status { get; set; }
        public int need_notif { get; set; }
        public string custom_f { get; set; }

        public string Name => purchaser_nominative;

        public string Phone => purchaser_phone;

        public string Date => _gate.GetDataTimeFromTimeStamp(double.Parse(checkin_ts));

        public string Email => purchaser_mail;

        public string Status => status;

        public TakeAwayReservationCustomF CustomF { get; set; }
        public string Print => need_notif == 2 ? "Yes" : "No";

        private Gate _gate;

        public TakeAwayReservation()
        {
            _gate = new Gate();
        }
    }
}
