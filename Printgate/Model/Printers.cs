using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;

namespace Printgate.Model
{
    public class Printers : BaseModel
    {
        public ObservableCollection<string> _printerNames { get; set; }

        public ObservableCollection<string> PrinterNames
        {
            get { return _printerNames; }
            set { _printerNames = value; NotifyPropertyChanged("PrinterList"); }
        }

        public Printers()
        {
            UpdatePrinterList();
        }

        public void UpdatePrinterList()
        {
            var localPrintServer = new PrintServer();
            var printQueues = localPrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            var printerList = (from printer in printQueues select printer).ToList();

            var resultItems = new ObservableCollection<string>();
            foreach (var printer in printerList)
            {
                resultItems.Add(printer.Name);
            }
            PrinterNames = resultItems;
        }
    }
}
