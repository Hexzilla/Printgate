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

namespace Printgate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
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
            var comboBox = new ComboBox();
            comboBox.Margin = new Thickness(0, 0, 0, 16);
            comboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding("PrinterNames") { Source = viewModel });
            PrinterList.Children.Add(comboBox);
        }
    }
}
