using System;
using Caliburn.Micro;
using FeatureAdmin.Messages;

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
            ProgressBarStatus = message.Progress;
            TextStatus = message.Title;
        }

        public double ProgressBarStatus { get; private set; }

        public string TextStatus { get; private set; }
    }
}
