using System;
using System.Collections.Generic;
using System.Text;

namespace Printgate.Model
{
    public class TakeAwayReservationItem
    {
        public string name { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string option_name { get; set; }
        public string notes { get; set; }
    }
    public class TakeAwayReservationPrintData
    {
        private Gate _gate;
        public TakeAwayReservationPrintData()
        {
            _gate = new Gate();
        }
        public long id { get; set; }
        public string checkin_ts { get; set; }
        public string purchaser_nominative { get; set; }
        public string purchaser_phone { get; set; }
        public string purchaser_mail { get; set; }
        public string status { get; set; }
        public int need_notif { get; set; }
        public string purchaser_prefix { get; set; }
        public string purchaser_country { get; set; }
        public string purchaser_address { get; set; }
        public string total_to_pay { get; set; }
        public string tot_paid { get; set; }
        public string taxes { get; set; }
        public string pay_charge { get; set; }
        public string delivery_charge { get; set; }
        public string discount_val { get; set; }
        public string tip_amount { get; set; }
        public string notes { get; set; }
        public string conf_key { get; set; }
        public string payment_log { get; set; }
        public string payment_name { get; set; }
        public string payment_note { get; set; }
        public string payment_prenote { get; set; }
        public string payment_charge { get; set; }
        public string product_price { get; set; }
        public string product_notes { get; set; }
        public string product_name { get; set; }
        public string option_name { get; set; }
        public string group_title { get; set; }
        public string custom_f { get; set; }
        public List<TakeAwayReservationItem> items { get; set; }

        public string Date => _gate.GetDataTimeFromTimeStamp(double.Parse(checkin_ts));

        public TakeAwayReservationCustomF CustomF { get; set; }
    }
}
