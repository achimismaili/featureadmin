using Akka.Actor;
using FeatureAdmin.Actor.Messages;
using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Actors
{
    public class LocationViewModelSyncActor : ReceiveActor
    {
        private readonly ObservableCollection<Location> locations;

        public LocationViewModelSyncActor(ObservableCollection<Location> locations)
        {
            this.locations = locations;

            Receive<AddLocationMessage>(message => AddLocation(message));
            // Receive<RemoveLocationMessage>(message => RemoveLocation(message));
        }


        private void AddLocation(AddLocationMessage message)
        {
            if (message == null || message.Location == null)
            {
                //TODO log
                return;
            }

            var locationToAdd = message.Location;
            if (locations.Any(l => l.Id == locationToAdd.Id))
            {
                var existingLocation = locations.FirstOrDefault(l => l.Id == locationToAdd.Id);
                locations.Remove(existingLocation);
            }

            locations.Add(locationToAdd);
        }
    }
}
