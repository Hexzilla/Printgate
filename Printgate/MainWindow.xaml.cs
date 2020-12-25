using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Printing;
using Printgate.ViewModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace Printgate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();
            viewModel.Printers.PropertyChanged += OnPrinterListChanged;

            DataContext = viewModel;

            //Task.Run((Action)MyFunction);
            //Updater.Start();

            for (var i = 0; i < 5; i++)
                AddPrinter();
        }

        private void AddPrinterButton_Click(object sender, RoutedEventArgs e)
        {
            AddPrinter();
        }

        private void AddPrinter()
        {
            int printerIndex = PrinterList.Children.Count;

            // Create New Combobox
            var comboBox = new ComboBox();
            comboBox.Margin = new Thickness(0, 0, 0, 16);

            // Add Selected Value Data Source
            int selectedPrinterIndex = viewModel.Settings.AddTakeAwayPrinter(printerIndex);
            var bindPath = $"Settings.TakeAwayPrinters[{selectedPrinterIndex}]";
            var bindingSelectedPrinter = new Binding(bindPath) { Source = viewModel };
            comboBox.SetBinding(ComboBox.SelectedValueProperty, bindingSelectedPrinter);

            var bindingNames = new Binding("Printers.PrinterNames") { Source = viewModel };
            comboBox.SetBinding(ComboBox.ItemsSourceProperty, bindingNames);
            
            comboBox.DropDownOpened += new EventHandler((object sender, EventArgs e) =>
            {
                viewModel.Printers.UpdatePrinterList();
            });

            comboBox.Tag = printerIndex;
            PrinterList.Children.Add(comboBox);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveSettings();
        }

        private void OnPrinterListChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PrinterList")
            {
                /*foreach (var item in PrinterList.Children)
                {
                    var comboBox = item as ComboBox;
                    var selectedValue = (string)comboBox.SelectedValue;
                    if (selectedValue != null)
                    {
                        var printerName = viewModel.Printers.PrinterNames.FirstOrDefault(it => it == selectedValue);
                        comboBox.SelectedValue = printerName;
                    }
                }*/
            }
        }
    }
}
