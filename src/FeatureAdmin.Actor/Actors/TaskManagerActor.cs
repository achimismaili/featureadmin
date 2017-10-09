using Akka.Actor;
using FeatureAdmin.Actor.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Actors
{
    public class TaskManagerActor : ReceiveActor
    {
        private IActorRef viewModelSyncActorRef;

        private readonly Dictionary<Guid, IActorRef> locationActors;

        public TaskManagerActor(IActorRef viewModelSyncActorRef)
        {
            locationActors = new Dictionary<Guid, IActorRef>();
            this.viewModelSyncActorRef = viewModelSyncActorRef;
             Receive<LoadTaskMessage>(message => LoadTask(message));
        }

        private void LoadTask(LoadTaskMessage message)
        {
            if (message == null || message.Location == null)
            {
                // TODO Log
                return;
            }

            var locationId = message.Location.Id;

            bool locationActorNeedsCreating = !locationActors.ContainsKey(locationId);

            if (locationActorNeedsCreating)
            {
                IActorRef newLocationActor =
                    Context.ActorOf(
                        Props.Create(() => new Backends.Actors.LocationManagerActor(viewModelSyncActorRef)),
                                     locationId.ToString());

                locationActors.Add(locationId, newLocationActor);
            }

            locationActors[locationId].Tell(new LoadLocationMessage(message.Location));
        }
    }
}
