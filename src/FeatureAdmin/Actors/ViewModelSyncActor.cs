using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models;
using System.Collections.ObjectModel;
using FeatureAdmin.Core.Messages;
using Caliburn.Micro;

namespace FeatureAdmin.Actors
{
    public class ViewModelSyncActor : ReceiveActor
    {
        private readonly IEventAggregator eventAggregator;

        public ViewModelSyncActor(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Receive<ItemUpdated<SPLocation>>(message => LocationUpdated(message));
            Receive<ItemUpdated<FeatureDefinition>>(message => FeatureDefinitionUpdated(message));

        }
        private void LocationUpdated(ItemUpdated<SPLocation> message)
        {
            if (message == null)
            {
                // TODO Log exception / catch null argument exception for message
                throw new ArgumentNullException("LocationUpdated - Did not expect null message!");
            }

            var location = message.Item as Location;
            eventAggregator.PublishOnUIThread(new ItemUpdated<Location>(location));
        }

        private void FeatureDefinitionUpdated(ItemUpdated<FeatureDefinition> message)
        {
            eventAggregator.PublishOnUIThread(message);
        }
    }
}
