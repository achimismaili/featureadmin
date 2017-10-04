using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using System.Collections.ObjectModel;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : Screen
    {
        private IEventAggregator eventAggregator;
        private ObservableCollection<Location> Locations;
        public LocationListViewModel(IEventAggregator eventAggregator)
        {
                Locations = new ObservableCollection<Location>();

                this.eventAggregator = eventAggregator;
                this.eventAggregator.Subscribe(this);
            }

        //public void Handle(SimpleMessage message)
        //{
        //    Messages.Add(message);
        //}

    }
}
