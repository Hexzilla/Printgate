using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Printing;
using System.Printing.IndexedProperties;
using System.Collections;
using System.Diagnostics;
using Printgate.Model;

namespace Printgate.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> printerNames { get; set; }

        public ObservableCollection<string> PrinterNames
        {
            get { return printerNames; }
            set { printerNames = value; OnPropertyChanged("PrinterNames"); }
        }

        private string selectedPrinter;

        public string SelectedPrinter
        {
            get { return selectedPrinter; }
            set
            {
                selectedPrinter = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private GateSettings _settings = new GateSettings();
        public GateSettings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                OnPropertyChanged("Settings");
            }
        }

        private readonly Gate restaurant;

        public MainWindowViewModel()
        {
            printerNames = new ObservableCollection<string>();

            var localPrintServer = new PrintServer();
            var printQueues = localPrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            var printerList = (from printer in printQueues select printer).ToList();
            
            foreach (var printer in printerList)
            {
                PrinterNames.Add(printer.Name);
            }

            this.restaurant = new Gate(Settings);
        }

        public void OnPropertyChanged(string propname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
        }
    }
}
