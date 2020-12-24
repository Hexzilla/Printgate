using System;
using System.Collections.Generic;
using System.Text;

namespace Printgate.Model
{
    public class GateSettings
    {
        public string JoomlaServer { get; set; }
        public string TableReservationPrinterName { get; set; }
        public List<string> TakeAwayPrinters{ get; set; }
        public string BeautifulPrinterName { get; set; }

        public bool ExportXml { get; set; }
        public bool AlarmNoConnection { get; set; }
        public bool WelcomeMessage { get; set; }
        public bool FullAddressMap { get; set; }
        public bool TableReservationPopup { get; set; }
        public bool TableReservationAlarm { get; set; }
        public bool FoodReservationPopup { get; set; }
        public bool FoodReservationAlarm { get; set; }
        public bool HotelReservationPopup { get; set; }
        public bool HotelReservationAlarm { get; set; }

        public string TableWelcomeMessage { get; set; }
        public string FoodEnjoyMessage { get; set; }
        public string RoomBookingMessage { get; set; }

        public GateSettings()
        {
            JoomlaServer = "http://localhost/site-src";
            ExportXml = true;
            AlarmNoConnection = true;
            HotelReservationPopup = true;
            HotelReservationAlarm = true;
            TableWelcomeMessage = "Hello Boys!";
        }
    }
}
