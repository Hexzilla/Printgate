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
using Printgate.Model;

namespace Printgate.View
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            AddTakeAwayPrinters();

            foreach (var item in CheckBoxGroup.Children)
            {
                if (item.GetType() == typeof(CheckBox))
                {
                    (item as CheckBox).Checked += CheckBox_Changed;
                    (item as CheckBox).Unchecked += CheckBox_Changed;
                }
            }
        }

        private void AddTakeAwayPrinters()
        {
            var printers = new List<TakeAwayPrinter>(viewModel.Settings.TakeAwayPrinters);
            foreach (var printer in printers)
            {
                AddPrinter(printer.CategoryName, printer.PrinterName);
            }

            foreach (var item in PrinterList.Children)
            {
                var grid = item as Grid;
                foreach (var combo in grid.Children)
                {
                    if (combo.GetType() == typeof(ComboBox))
                    {
                        (combo as ComboBox).SelectionChanged += ComboBox_SelectionChanged;
                    }
                }
            }
        }

        private void AddPrinterButton_Click(object sender, RoutedEventArgs e)
        {
            AddPrinter();
        }

        private void AddPrinter(string selectedCategory = "", string selectedPrinter = "")
        {
            // Create Stackpanel
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            // Create Food Category Combobox
            var categories = new ComboBox();
            categories.Margin = new Thickness(0, 0, 10, 16);
            categories.DisplayMemberPath = "Name";
            categories.SetBinding(ComboBox.ItemsSourceProperty, new Binding("FoodCategories") { Source = viewModel });
            categories.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                var found = viewModel.FoodCategories.First(it => it.Name == selectedCategory);
                if (found != null)
                    categories.SelectedValue = found;
            }
            Grid.SetColumn(categories, 0);
            grid.Children.Add(categories);

            // Create Printers Combobox
            var printers = new ComboBox();
            printers.Margin = new Thickness(10, 0, 0, 16);
            printers.SetBinding(ComboBox.ItemsSourceProperty, new Binding("Printers.PrinterNames") { Source = viewModel });
            printers.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(selectedPrinter))
                printers.SelectedValue = selectedPrinter;
            printers.DropDownOpened += new EventHandler((object sender, EventArgs e) =>
            {
                viewModel.Printers.GetPrinterList();
            });
            Grid.SetColumn(printers, 1);
            grid.Children.Add(printers);

            PrinterList.Children.Add(grid);
        }

        private void SaveSettings()
        {
            viewModel.SaveSettings(PrinterList.Children);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();

        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveSettings();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveSettings();
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SaveSettings();
        }
    }
}
