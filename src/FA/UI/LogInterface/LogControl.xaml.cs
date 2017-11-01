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
                           .WriteTo.TextWriter(logWriter, outputTemplate:
        "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}")
                           .CreateLogger();
            LogStartedMessageWithDate();
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
            LogStartedMessageWithDate();
        }

        private void LogStartedMessageWithDate()
        {
            logWriter.WriteLine("Log started on {0}", System.DateTime.Now.ToShortDateString());
        }
    }
}
