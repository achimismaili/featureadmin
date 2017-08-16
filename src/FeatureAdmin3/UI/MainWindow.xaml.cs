using FeatureAdmin3.Repository;
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

namespace FeatureAdmin3.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingAdorner.IsAdornerVisible = true;
            VisibleScreen.IsEnabled = false;
            _viewModel.Load();
            LoadingAdorner.IsAdornerVisible = false;
            VisibleScreen.IsEnabled = true;
        }
    }
}
