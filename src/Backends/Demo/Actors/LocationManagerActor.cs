using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Actors
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

            Receive<LocationUpdated>(message => LocationUpdated(message));
        }

        private void LoadLocation(LoadLocationQuery message)
        {
            _log.Debug("Entered LocationManager-LoadLocation");

            _locationActorChild.Tell(message);
        }

        private void LocationUpdated(LocationUpdated message)
        {
            if (message == null || message.Location == null)
            {
                _log.Error("empty LocationUpdated message returned!");
                return;
            }

            if (message.Location.Id == myLocation ||
                !message.Location.CanHaveChildren ||
                (myLocation == Guid.Empty && message.Location.Scope == Core.Models.Enums.Scope.Farm)
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
