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
        private IFeatureRepository repo;

        public MainWindow()
            :this(new FeatureRepository())
        { }

        public MainWindow(IFeatureRepository featureRepository)
        {
            InitializeComponent();
        }

     private void ReloadRepository()
        {
            LoadingAdorner.IsAdornerVisible = true;
            VisibleScreen.IsEnabled = false;
            repo.Reload();
            LoadingAdorner.IsAdornerVisible = false;
            VisibleScreen.IsEnabled = true;
        }

    }
}
