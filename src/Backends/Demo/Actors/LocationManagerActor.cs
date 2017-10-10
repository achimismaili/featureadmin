using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using FeatureAdmin.Backends.Messages;
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

            Receive<LoadLocationMessage>(message => LoadLocation(message));

            Receive<LookedUpLocationMessage>(
                message =>
                {
                    location = message.Location;

                    var stockPriceMessage = new LocationLoadedMessage(location);

                    viewModelSyncActor.Tell(stockPriceMessage);
                }
                );
        }

        private void LoadLocation(LoadLocationMessage message)
        {
            _log.Debug("Entered LocationManager-LoadLocation");
            _locationActorChild.Tell(
                new LookUpLocationMessage(message.Location));
        }
    }
}
