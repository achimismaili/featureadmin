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
        private readonly HashSet<IActorRef> _subscribers;

        private readonly IActorRef _locationActorChild;
        private readonly IActorRef viewModelSyncActor;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private Location location;
        public LocationManagerActor(IActorRef viewModelSyncActor)
        {
            _subscribers = new HashSet<IActorRef>();

            _locationActorChild = Context.ActorOf(Context.DI().Props<LocationActor>());

            this.viewModelSyncActor = viewModelSyncActor;

            Receive<LocationQuery>(message => LoadLocation(message));

            Receive<LocationUpdated>(
                message =>
                {
                    viewModelSyncActor.Tell(message);
                }
                );
        }

        private void LoadLocation(LocationQuery message)
        {
            _log.Debug("Entered LocationManager-LoadLocation");
            _locationActorChild.Tell(message);
        }
    }
}
