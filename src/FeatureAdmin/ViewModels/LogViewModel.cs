using Caliburn.Micro;
using FeatureAdmin.Messages;
using System.Text;

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

            LogStartedMessage();
        }

        public void Handle(LogMessage message)
        {
            Logs.Add(message);
        }

        public BindableCollection<LogMessage> Logs { get; }

        public void CopyLog()
        {
            var log = new StringBuilder();

            foreach (LogMessage lm in Logs)
            {
                log.Append(string.Format("{0}\t{1}\t{2}\n", 
                    lm.Time.ToString(), 
                    lm.ShortLevel, 
                    lm.Text));
            }

            System.Windows.Clipboard.SetText(log.ToString());
        }

        public void ClearLog()
        {
            Logs.Clear();
            LogStartedMessage();
        }

        private void LogStartedMessage()
        {
            var startMsg = new LogMessage(Core.Models.Enums.LogLevel.Information, 
                "Feature Admin Log");

            Logs.Add(startMsg);
        }

    }
}
