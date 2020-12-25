using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace Printgate.Model
{
    public class TakeAwayPrinterListProperty : INotifyCollectionChanged
    {
        private ObservableCollection<TakeAwayPrinter> printers;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        internal TakeAwayPrinterListProperty(ObservableCollection<TakeAwayPrinter> printers)
        {
            this.printers = printers;
            this.printers.CollectionChanged += (s, e) =>
            {
                CollectionChanged?.Invoke(s, e);
            };
        }

        public TakeAwayPrinter this[int index]
        {
            get
            {
                if (printers.Count > index)
                {
                    return printers[index];
                }
                else
                {
                    var printer = new TakeAwayPrinter();
                    printers.Add(printer);
                    return printer;
                }
            }
            set
            {
                if (printers.Count > index)
                {
                    printers[index] = value;
                }
                else
                {
                    printers.Insert(index, value);
                }
            }
        }
    }
}
