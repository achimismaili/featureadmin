using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Actors
{
    public class TaskManagerActor : ReceiveActor
    {
        private IActorRef viewModelSyncActorRef;
        private readonly IActorRef featureDefinitionActor;
        private readonly Dictionary<Guid, IActorRef> locationActors;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public TaskManagerActor(IActorRef viewModelSyncActorRef)
        {
            locationActors = new Dictionary<Guid, IActorRef>();
            this.viewModelSyncActorRef = viewModelSyncActorRef;

            featureDefinitionActor =
                    Context.ActorOf(
                        Props.Create(() => new FeatureDefinitionManagerActor(viewModelSyncActorRef)),
                                     "FeatureDefinitionManagerActor");


            Receive<LoadLocationQuery>(message => LoadTask(message));

            Receive<ItemUpdated<SPLocation>>(message => LocationUpdated(message));

            Receive<LoadFeatureDefinitionQuery>(message => LoadFeatureDefinitions(message));
        }

        private void LoadFeatureDefinitions(LoadFeatureDefinitionQuery message)
        {
            featureDefinitionActor.Tell(message);
        }

        private void LoadTask(LoadLocationQuery message)
        {
            _log.Debug("Entered TaskManager-LoadTask");
            if (message == null || message.SPLocation == null)
            {
                _log.Error("LoadTask message or location was null");
                return;
            }

            var locationId = message.SPLocation.Id;

            bool locationActorNeedsCreating = !locationActors.ContainsKey(locationId);

            if (locationActorNeedsCreating)
            {
                IActorRef newLocationActor =
                    Context.ActorOf(
                        Props.Create(() => new LocationManagerActor(viewModelSyncActorRef, locationId)),
                                     locationId.ToString());

                locationActors.Add(locationId, newLocationActor);

                // newLocationActor.Tell(message);
            }

            locationActors[locationId].Tell(message);
        }

        /// <summary>
        /// This message is received from a location manager actor when child location is loaded. 
        /// this method will send a load task to the correctly responsible actor 
        /// </summary>
        /// <param name="message"></param>
        private void LocationUpdated(ItemUpdated<SPLocation> message)
        {
            if (message == null || message.Item == null)
            {
                _log.Error("empty LocationUpdated message returned!");
                return;
            }

            LoadTask(new LoadLocationQuery(message.Item));
        }
    }
}
