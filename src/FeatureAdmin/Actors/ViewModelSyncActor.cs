using Akka.Actor;
using System;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Messages;
using Caliburn.Micro;
using FeatureAdmin.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Actors
{
    public class ViewModelSyncActor : ReceiveActor
    {
        private readonly IEventAggregator eventAggregator;

        public ViewModelSyncActor(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Receive<ItemUpdated<IEnumerable<Location>>>(message => LocationUpdated(message));
            Receive<ItemUpdated<FeatureDefinition>>(message => FeatureDefinitionUpdated(message));

        }
        private void LocationUpdated(ItemUpdated<IEnumerable<Location>> message)
        {
            if (message == null || message.Item == null)
            {
                // TODO Log exception / catch null argument exception for message
                throw new ArgumentNullException("LocationUpdated - Did not expect null message!");
            }

            var locations = message.Item;

            // publish locations
            eventAggregator.PublishOnUIThread(message);

            var features = locations.SelectMany(l => l.ActivatedFeatures);

            var featureDefinitions = features.Select(f => f.Definition).Distinct();

            foreach (FeatureDefinition fd in featureDefinitions)
            {
                    foreach (var locationIdToAdd in features.Where(f => f.Definition == fd).Select(f => f.LocationId))
                    {
                    fd.ToggleActivatedFeature(locationIdToAdd, true);
                    }
            }

            eventAggregator.PublishOnUIThread(new ItemUpdated<IEnumerable<FeatureDefinition>>(featureDefinitions));
        }

        private void FeatureDefinitionUpdated(ItemUpdated<FeatureDefinition> message)
        {
            eventAggregator.PublishOnUIThread(message);
        }
    }
}
