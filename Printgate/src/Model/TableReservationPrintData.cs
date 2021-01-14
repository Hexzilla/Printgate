using System;
using System.Collections.Generic;
using System.Text;

namespace Printgate.Model
{
    public class TableReservationMenuList
    {
        public string menu_name { get; set; }
        public string menu_quantity { get; set; }
    }
    public class TableReservationPrintData
    {
        private Gate _gate;
        public TableReservationPrintData()
        {
            _gate = new Gate();
        }
        public long id { get; set; }
        public string checkin_ts { get; set; }
        public string purchaser_nominative { get; set; }
        public string purchaser_phone { get; set; }
        public string purchaser_mail { get; set; }
        public string status { get; set; }
        public string need_notif { get; set; }
        public string people { get; set; }
        public string purchaser_prefix { get; set; }
        public string purchaser_country { get; set; }
        public string bill_closed { get; set; }
        public string bill_value { get; set; }
        public string deposit { get; set; }
        public string tot_paid { get; set; }
        public string discount_val { get; set; }
        public string tip_amount { get; set; }
        public string conf_key { get; set; }
        public string table_name { get; set; }
        public string room_name { get; set; }
        public string room_description { get; set; }
        public string menu_name { get; set; }
        public string menu_quantity { get; set; }
        public string user_name { get; set; }
        public string user_uname { get; set; }
        public string user_email { get; set; }
        public List<TableReservationMenuList> menus_list { get; set; }
        public string custom_f { get; set; }

        public string Date => _gate.GetDataTimeFromTimeStamp(double.Parse(checkin_ts));

        public TableReservationCustomF CustomF { get; set; }

    }
}
