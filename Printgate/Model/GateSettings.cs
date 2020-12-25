using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Printgate.Model
{
    public class GateSettings : BaseModel
    {
        public string JoomlaServer { get; set; }
        public string TableReservationPrinterName { get; set; }

        public ObservableCollection<string> _takeAwayPrinters = new ObservableCollection<string>();
        public ObservableCollection<string> TakeAwayPrinters
        {
            get { return _takeAwayPrinters; }
            set
            {
                _takeAwayPrinters = value;
                NotifyPropertyChanged("TakeAwayPrinters");
            }
        }

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

            TakeAwayPrinters.Add("Fax");
            TakeAwayPrinters.Add("Fax");
        }

        public int AddTakeAwayPrinter(int index)
        {
            if (TakeAwayPrinters.Count <= index)
            {
                TakeAwayPrinters.Add("");
                return TakeAwayPrinters.Count - 1;
            }
            else
            {
                return index;
            }
        }
    }
}
