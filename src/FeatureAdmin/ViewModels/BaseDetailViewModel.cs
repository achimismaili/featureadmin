using Caliburn.Micro;
using System.Windows;

namespace FeatureAdmin.ViewModels
{
    public class BaseDetailViewModel : Screen
    {
        protected IEventAggregator eventAggregator;

        public bool ItemSelected { get; set; }

        public BaseDetailViewModel(IEventAggregator eventAggregator) 
        {
            ItemSelected = false;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }

        protected void copyToClipBoard(string textToCopy)
        {
            Clipboard.SetText(textToCopy);
        }
    }
}
