using Caliburn.Micro;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : Screen
    {
        private IEventAggregator eventAggregator;
        public ObservableCollection<Location> Locations;
        public LocationListViewModel(IEventAggregator eventAggregator)
        {
                Locations = new ObservableCollection<Location>();

                this.eventAggregator = eventAggregator;
                this.eventAggregator.Subscribe(this);
            }

        public void Handle(LocationAdd location)
        {
            if (location == null)
            {
                return;
            }

            var existingLocation = Locations.FirstOrDefault(l => l.Id == location.Id);

            if (existingLocation != null)
            {
                Locations.Remove(existingLocation);
            }

            Locations.Add(location);
        }

    }
}
