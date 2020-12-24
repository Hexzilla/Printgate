using Newtonsoft.Json.Linq;
using Printgate.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Printgate
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window
    {
        private Gate gate;

        ObservableCollection<TableReservation> mTableReservations = new ObservableCollection<TableReservation>();
        ObservableCollection<TakeAwayReservation> mTakeAwayReservations = new ObservableCollection<TakeAwayReservation>();
        long maxTableReservationId = 0;
        long maxTakeAwayReservationId = 0;
        string url;
        string tableWelcomeMessage;
        string takeawayWelcomeMessage;


        public ReservationView()
        {
            InitializeComponent();
            ProgressBar.Visibility = Visibility.Hidden;

            SetTableWelcomeMessage();
            SetTakeAwayWelcomeMessage();
            
            //Set data to list view
            //GetTableDataFromServer();
            TableReservationListView.ItemsSource = mTableReservations;

            //GetTakeAwayDataFromServer();
            TakeAwayReservationListView.ItemsSource = mTakeAwayReservations;
        }
        public void SetGate(Gate gate)
        {
            this.gate = gate;
        }

        private void SetTableWelcomeMessage()
        {
            tableWelcomeMessage = "welcome!";
        }
        private void SetTakeAwayWelcomeMessage()
        {
            takeawayWelcomeMessage = "welcome!";
        }

        private void BeforeAsync()
        {
            ProgressBar.Visibility = Visibility.Visible;
            Mouse.OverrideCursor = Cursors.Wait;
            TakeAwayConfirmButton.IsEnabled = false;
            TakeAwayRejectButton.IsEnabled = false;
            TakeAwayWelcomeButton.IsEnabled = false;
            TableConfirmButton.IsEnabled = false;
            TableRejectButton.IsEnabled = false;
            TableWelcomeButton.IsEnabled = false;
        }
        private void AfterAsync()
        {
            ProgressBar.Visibility = Visibility.Hidden;
            Mouse.OverrideCursor = null;
            TakeAwayConfirmButton.IsEnabled = true;
            TakeAwayRejectButton.IsEnabled = true;
            TakeAwayWelcomeButton.IsEnabled = true;
            TableConfirmButton.IsEnabled = true;
            TableRejectButton.IsEnabled = true;
            TableWelcomeButton.IsEnabled = true;
        }

        private string GetDataTimeFromTimeStamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp).ToString();
        }

        private TableReservation SelectedTableReservationItemId()
        {
            TableReservation item;
            try
            {
                item = TableReservationListView.SelectedItems[0] as TableReservation;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            
            return item;
        }


        //Table reservation
        private void TableConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTableReservationItemId();
            if (item != null)
            { 
                url = url + "&id=" + item.id + "&action=confirm" + "&type=restaurant";
                SetTableReservationToServer(url, item);
            }
        }

        private void TableRejectButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTableReservationItemId();
            if (item != null)
            {
                url = url + "&id=" + item.id + "&action=reject" + "&type=restaurant";
                SetTableReservationToServer(url, item);
            }
        }

        private void TableWelcomeButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTableReservationItemId();
            if (item != null)
            {
                url = url + "&id=" + item.id + "&action=welcome" + "&type=restaurant" + "&message=" + tableWelcomeMessage;
                SetTableReservationToServer(url, item);
            }
        }

        private TakeAwayReservation SelectedTakeAwayReservationItemId()
        {
            TakeAwayReservation item;
            try
            {
                item = TakeAwayReservationListView.SelectedItems[0] as TakeAwayReservation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

            return item;
        }
        //Take away reservation
        private void TakeAwayConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTakeAwayReservationItemId();
            if (item != null)
            {
                url = url + "&id=" + item.id + "&action=confirm" + "&type=takeaway";
                SetTakeAwayReservationToServer(url, item);
            }
        }

        private void TakeAwayRejectButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTakeAwayReservationItemId();
            if (item != null)
            {
                url = url + "&id=" + item.id + "&action=reject" + "&type=takeaway";
                SetTakeAwayReservationToServer(url, item);
            }
        }

        private void TakeAwayWelcomeButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTakeAwayReservationItemId();
            if (item != null)
            {
                url = url + "&id=" + item.id + "&action=welcome" + "&type=takeaway" + "&message=" + takeawayWelcomeMessage;
                SetTakeAwayReservationToServer(url, item);
            }
        }

    }
}
