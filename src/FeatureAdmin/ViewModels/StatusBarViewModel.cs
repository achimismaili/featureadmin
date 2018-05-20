using System;
using Caliburn.Micro;
using FeatureAdmin.Messages;
using System.Threading.Tasks;

namespace FeatureAdmin.ViewModels
{
    public class StatusBarViewModel : Screen, IHandle<ProgressMessage>
    {
        private IEventAggregator eventAggregator;
       
        public StatusBarViewModel(IEventAggregator eventAggregator)
        {
                this.eventAggregator = eventAggregator;
                this.eventAggregator.Subscribe(this);
            }

        public void Handle(ProgressMessage message)
        {
            // no await preceeded, because async only needed to turn of status bar after 10 s when 100%
            SetTaskBar(message);
        }

            public async Task SetTaskBar(ProgressMessage message)
        {
            ProgressBarStatus = message.Progress;
            TextStatus = message.Title;

            if (message.Progress >= 1d)
            {
                await PutTaskDelay();

                if (TextStatus.Equals(message.Title))
                {
                    ProgressBarStatus = 0d;
                    TextStatus = string.Empty;
                }
            }
        }

        public double ProgressBarStatus { get; private set; }

        public string TextStatus { get; private set; }


        async Task PutTaskDelay()
        {
            await Task.Delay(10000);
        }
    }
}
