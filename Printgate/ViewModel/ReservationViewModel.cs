using Printgate.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Printgate.ViewModel
{
    class ReservationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TableReservations tableItems;
        private TakeAwayReservations takeawayItems;

        private GateSettings settings;
        private Gate gate;

        private long maxTableReservationId = 0;
        private long maxTakeAwayReservationId = 0;

        public ReservationViewModel()
        {
            settings = new GateSettings();
            gate = new Gate(settings);

            tableItems = new TableReservations();
            takeawayItems = new TakeAwayReservations();

            gate.GetTableDataFromServer(GenerateUrl(maxTableReservationId.ToString(), "restaurant"), tableItems);
            gate.GetTakeAwayDataFromServer(GenerateUrl(maxTakeAwayReservationId.ToString(), "takeaway"), takeawayItems);
        }

        public ObservableCollection<TableReservation> TableItems
        {
            get { return tableItems.Items; }
            set { tableItems.Items = value; OnPropertyChanged("TableItems"); }
        }

        public ObservableCollection<TakeAwayReservation> TakeAwayItems
        {
            get { return takeawayItems.Items; }
            set { takeawayItems.Items = value; OnPropertyChanged("TakeAwayItems"); }
        }

        public void OnPropertyChanged(string propname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
        }

        public async Task<bool> SendTableDataToServer(TableReservation item, string action)
        {
            var url = GenerateUrl(item.id.ToString(), action, "restaurant", settings.TableWelcomeMessage);
            var result = await gate.SetTableReservationToServer(url);
            if (result != null)
            {
                TableItems.Remove(item);
                return true;
            }
            return false;
        }
        public async Task<bool> SendTakeAwayDataToServer(TakeAwayReservation item, string action)
        {
            var url = GenerateUrl(item.id.ToString(), action, "takeaway", settings.FoodEnjoyMessage);
            var result = await gate.SetTakeAwayReservationToServer(url);
            if (result != null)
            {
                TakeAwayItems.Remove(item);
                return true;
            }
            return false;
        }

        private string GetServerUrl()
        {
            return string.Format("{0}/index.php?option=com_api&app=vikrestaurants&resource=reservation&format=raw", settings.JoomlaServer);
        }
        private string GenerateUrl(string id = "", string action = "", string type = "", string message = "")
        {
            return GetServerUrl() + "&id=" + id + "&action=" + action + "&type=" + type + "&message=" + message;
        }
        private string GenerateUrl(string id, string type)
        {
            return GetServerUrl() + "&id=" + id + "&type=" + type;
        }
    }
}
