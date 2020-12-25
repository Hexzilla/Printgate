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
    class MainWindowViewModel
    {
        public Printers Printers { get; set; } = new Printers();

        public GateSettings Settings { get; set; } = new GateSettings();

        private readonly Gate gate;

        public MainWindowViewModel()
        {
            this.gate = new Gate(Settings);
        }
    }
}
