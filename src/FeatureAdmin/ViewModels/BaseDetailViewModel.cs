using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Windows;

namespace FeatureAdmin.ViewModels
{
    public class BaseDetailViewModel<T> : Screen, IHandle<ItemSelected<T>> where T : BaseItem
    {
        protected IEventAggregator eventAggregator;

        public T Item { get; protected set; }

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

        public void FilterLocation()
        {
            //TODO check Item for null
            var searchFilter = new SetSearchFilter<Location>(Item.Id.ToString());
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        public void FilterFeature()
        {
            //TODO check Item for null
            var searchFilter = new SetSearchFilter<FeatureDefinition>(Item.Id.ToString());
            eventAggregator.BeginPublishOnUIThread(searchFilter);
        }

        public void Handle(ItemSelected<T> message)
        {
            Item = message.Item;
            ItemSelected = message.Item != null;
        }
    }
}
