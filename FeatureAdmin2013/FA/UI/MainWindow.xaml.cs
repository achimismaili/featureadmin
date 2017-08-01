using Serilog;
using System;
using System.Collections.Generic;
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

namespace FA.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            _viewModel = viewModel;
            this.DataContext = viewModel;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Load();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Load();
        }

        private void click_LogTest(object sender, RoutedEventArgs e)
        {
            Log.Warning("It is now {Now}", DateTime.Now.ToShortTimeString());
        }
    }
}
