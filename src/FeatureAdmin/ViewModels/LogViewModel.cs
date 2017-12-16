using Caliburn.Micro;
using FeatureAdmin.Common;
using FeatureAdmin.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.ViewModels
{
    public class LogViewModel : Screen, IHandle<LogMessage>
    {
        private readonly IEventAggregator eventAggregator;

        public LogViewModel(IEventAggregator eventAggregator)
        {
            Logs = new BindableCollection<LogMessage>();

            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
        }

        public void Handle(LogMessage message)
        {
            Logs.Add(message);
        }

        public BindableCollection<LogMessage> Logs { get; }
       
        //private void LogBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var h = logScrollViewer.ContentHorizontalOffset;
        //    this.logScrollViewer.ScrollToEnd();
        //    logScrollViewer.ScrollToHorizontalOffset(h);
        //}

        //private void click_CopyToClipboard(object sender, RoutedEventArgs e)
        //{
        //    Clipboard.SetText(LogBox.Text);
        //}

        //public void Clear()
        //{
        //    Logs.Clear();
        //    LogStartedMessageWithDate();
        //}

        //private void LogStartedMessageWithDate()
        //{
        //    logWriter.WriteLine("Log started on {0}", System.DateTime.Now.ToShortDateString());
        //}

    }
}
