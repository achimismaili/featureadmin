using Caliburn.Micro;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace FeatureAdmin.ViewModels
{
    public class LocationListViewModel : Screen, IHandle<LocationUpdated>
    {
        public ObservableCollection<Location> Locations { get; private set; }

        private IEventAggregator eventAggregator;

        public LocationListViewModel(IEventAggregator eventAggregator)
        {

                Locations = new ObservableCollection<Location>();

                this.eventAggregator = eventAggregator;
                this.eventAggregator.Subscribe(this);
            }

        public void Handle(LocationUpdated message)
        {
            if (message == null || message.Location == null)
            {
                //TODO log
                return;
            }

            var locationToAdd = message.Location;
            if (Locations.Any(l => l.Id == locationToAdd.Id))
            {
                var existingLocation = Locations.FirstOrDefault(l => l.Id == locationToAdd.Id);
                Locations.Remove(existingLocation);
            }

            Locations.Add(locationToAdd);

        }
    }
}
