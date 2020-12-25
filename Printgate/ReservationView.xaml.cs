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
using Printgate.ViewModel;

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

        public ReservationView()
        {
            InitializeComponent();
            ProgressBar.Visibility = Visibility.Hidden;

            gate.GetTableDataFromServer(mTableReservations);
            TableReservationListView.ItemsSource = mTableReservations;

            gate.GetTakeAwayDataFromServer(mTakeAwayReservations);
            TakeAwayReservationListView.ItemsSource = mTakeAwayReservations;
        }

        public void SetGate(Gate gate)
        {
            this.gate = gate;
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
                gate.SendTableDataToServer(item, "confirm");
            }
        }

        private void TableRejectButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTableReservationItemId();
            if (item != null)
            {
                gate.SendTableDataToServer(item, "reject");
            }
        }

        private void TableWelcomeButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTableReservationItemId();
            if (item != null)
            {
                gate.SendTableDataToServer(item, "welcome");
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
                gate.SendTakeAwayDataToServer(item, "confirm");
            }
        }

        private void TakeAwayRejectButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTakeAwayReservationItemId();
            if (item != null)
            {
                gate.SendTakeAwayDataToServer(item, "reject");
            }
        }

        private void TakeAwayWelcomeButton_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedTakeAwayReservationItemId();
            if (item != null)
            {
                gate.SendTakeAwayDataToServer(item, "welcome");
            }
        }

    }
}
