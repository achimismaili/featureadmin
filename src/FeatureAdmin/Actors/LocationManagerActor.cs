using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using System;

namespace FeatureAdmin.Actors
{
    public class LocationManagerActor : ReceiveActor
    {
        private Guid myLocation;
        private readonly IActorRef _locationActorChild;
        private readonly IActorRef viewModelSyncActor;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public LocationManagerActor(IActorRef viewModelSyncActor, Guid myLocation)
        {
            _locationActorChild = Context.ActorOf(Context.DI().Props<LocationActor>());

            this.myLocation = myLocation;

            this.viewModelSyncActor = viewModelSyncActor;

            Receive<LoadLocationQuery>(message => LoadLocation(message));

            Receive<ItemUpdated<SPLocation>>(message => LocationUpdated(message));
        }

        private void LoadLocation(LoadLocationQuery message)
        {
            _log.Debug("Entered LocationManager-LoadLocation");

            _locationActorChild.Tell(message);
        }

        private void LocationUpdated(ItemUpdated<SPLocation> message)
        {
            if (message == null || message.Item == null)
            {
                _log.Error("empty LocationUpdated message returned!");
                return;
            }

            if (message.Item.Id == myLocation ||
                !message.Item.CanHaveChildren ||
                (myLocation == Guid.Empty && message.Item.Scope == Core.Models.Enums.Scope.Farm)
                )

                // report loaded location to viewmodelsync actor , if this is mylocation or if this location cannot have children
                viewModelSyncActor.Tell(message);

           else
            {            
                // report child location to task manager to get it processed by different actor
                Context.Parent.Tell(message);
            }
        }
    }
}
