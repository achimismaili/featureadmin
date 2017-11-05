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

            Receive<ItemUpdated<Location>>(message => LocationUpdated(message));
        }

        private void LoadLocation(LoadLocationQuery message)
        {
            _log.Debug("Entered LocationManager-LoadLocation");

            _locationActorChild.Tell(message);
        }

        private void LocationUpdated(ItemUpdated<Location> message)
        {
            if (message == null || message.Item == null)
            {
                _log.Error("empty LocationUpdated message returned!");
                return;
            }

            if (message.Item.Scope == Core.Models.Enums.Scope.WebApplication && message.Item.Id != myLocation)
            {
                // report other web applications to task manager to get it processed by different actor
                Context.Parent.Tell(message);
            }
            else
            {
                // report loaded location to viewmodelsync actor , if this is mylocation or if this location cannot have children
                viewModelSyncActor.Tell(message);
            }
        }
    }
}
