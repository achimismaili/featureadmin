using Akka.Actor;
using Akka.DI.Core;
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

        private Location location;
        public LocationManagerActor(IActorRef viewModelSyncActor)
        {
            _subscribers = new HashSet<IActorRef>();

            _locationActorChild = Context.ActorOf(Context.DI().Props<LocationActor>());

            this.viewModelSyncActor = viewModelSyncActor;

            Receive<LoadLocationMessage>(message => _locationActorChild.Tell(message));

            Receive<LookedUpLocationMessage>(
                message =>
                {
                    location = message.Location;

                    var stockPriceMessage = new LocationLoadedMessage(location);

                    foreach (var subscriber in _subscribers)
                    {
                        subscriber.Tell(stockPriceMessage);
                    }
                }
                );
        }
    }
}
