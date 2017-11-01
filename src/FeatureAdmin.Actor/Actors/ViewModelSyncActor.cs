using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models;
using System.Collections.ObjectModel;
using FeatureAdmin.Core.Messages;

namespace FeatureAdmin.Actor.Actors
{
    public class ViewModelSyncActor : ReceiveActor
    {
        private readonly IEventAggregator eventAggregator;

        public ViewModelSyncActor(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Receive<LocationUpdated>(message => AddLocation(message));

        }
        private void AddLocation(LocationUpdated message)
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
