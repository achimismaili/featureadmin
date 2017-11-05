using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;

namespace FeatureAdmin.Actors
{
    public class FeatureDefinitionManagerActor : ReceiveActor
    {
        private readonly IActorRef featureDefinitionActorChild;
        private readonly IActorRef viewModelSyncActor;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public FeatureDefinitionManagerActor(IActorRef viewModelSyncActor)
        {
            featureDefinitionActorChild = Context.ActorOf(Context.DI().Props<FeatureDefinitionActor>());

            this.viewModelSyncActor = viewModelSyncActor;

            Receive<LoadFeatureDefinitionQuery>(message => LoadFeatureDefinitions(message));

            Receive<ItemUpdated<FeatureDefinition>>(message => FeatureDefinitionUpdated(message));
        }

        private void LoadFeatureDefinitions(LoadFeatureDefinitionQuery message)
        {
            _log.Debug("Entered LoadFeatureDefinitions");

            featureDefinitionActorChild.Tell(message);
        }

        private void FeatureDefinitionUpdated(ItemUpdated<FeatureDefinition> message)
        {
                viewModelSyncActor.Tell(message);
        }
    }
}
