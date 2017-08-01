using System.Windows;
using System.Windows.Controls;
using Serilog;

namespace FA.UI.LogInterface
{
    /// <summary>
    /// Interaction logic for LogControl.xaml
    /// </summary>
    public partial class LogControl : UserControl
    {
        public ControlWriter logWriter;

        public LogControl()
        {
            InitializeComponent();

            logWriter = new ControlWriter(LogBox);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                           .WriteTo.TextWriter(logWriter)
                           .CreateLogger();
            Log.Debug("This is a log entry asdfasdf ...");
        }

        private void LogBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var h = logScrollViewer.ContentHorizontalOffset;
            this.logScrollViewer.ScrollToEnd();
            logScrollViewer.ScrollToHorizontalOffset(h);
        }

        private void click_CopyToClipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(LogBox.Text);
        }

        private void click_Clear(object sender, RoutedEventArgs e)
        {
            LogBox.Clear();
        }
    }
}
