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

            Receive<LocationUpdated>(message => LocationUpdated(message));
            Receive<FeatureDefinitionUpdated>(message => FeatureDefinitionUpdated(message));

        }
        private void LocationUpdated(LocationUpdated message)
        {
            eventAggregator.PublishOnUIThread(message);
        }

        private void FeatureDefinitionUpdated(FeatureDefinitionUpdated message)
        {
            eventAggregator.PublishOnUIThread(message);
        }
    }
}
