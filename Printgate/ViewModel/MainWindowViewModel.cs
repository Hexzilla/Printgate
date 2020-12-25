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
using System.IO;
using Newtonsoft.Json;

namespace Printgate.ViewModel
{
    class MainWindowViewModel
    {
        public Printers Printers { get; set; } = new Printers();

        public GateSettings Settings { get; set; }

        private readonly Gate gate;

        public MainWindowViewModel()
        {
            Settings = LoadSettings() ?? new GateSettings();
            this.gate = new Gate(Settings);
        }

        internal void SaveSettings()
        {
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@"settings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, Settings);
            }
        }

        internal GateSettings LoadSettings()
        {
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(@"settings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (GateSettings)serializer.Deserialize(file, typeof(GateSettings));
            }
        }
    }
}
