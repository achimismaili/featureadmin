using Akka.Actor;
using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Messages;
using Caliburn.Micro;
using FeatureAdmin.ViewModels;

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
            if (message == null || message.Item == null)
            {
                // TODO Log exception / catch null argument exception for message
                throw new ArgumentNullException("LocationUpdated - Did not expect null message!");
            }

            var location = new LocationViewModel(message.Item);
            eventAggregator.PublishOnUIThread(new ItemUpdated<LocationViewModel>(location));
        }

        private void FeatureDefinitionUpdated(ItemUpdated<FeatureDefinition> message)
        {
            eventAggregator.PublishOnUIThread(message);
        }
    }
}
