using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace FeatureAdmin.Views
{
    /// <summary>
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : UserControl
    {
        public LogView()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)Logs.Items).CollectionChanged += new NotifyCollectionChangedEventHandler(Logs_CollectionChanged);
        }


        private void Logs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // var h = logScrollViewer.ContentHorizontalOffset;
                this.logScrollViewer.ScrollToEnd();
                logScrollViewer.ScrollToHorizontalOffset(73);
            }
        }
    }
}
