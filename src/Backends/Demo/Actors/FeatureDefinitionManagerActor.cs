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

            Receive<FeatureDefinitionUpdated>(message => FeatureDefinitionUpdated(message));
        }

        private void LoadFeatureDefinitions(LoadFeatureDefinitionQuery message)
        {
            _log.Debug("Entered LoadFeatureDefinitions");

            featureDefinitionActorChild.Tell(message);
        }

        private void FeatureDefinitionUpdated(FeatureDefinitionUpdated message)
        {
                viewModelSyncActor.Tell(message);
        }
    }
}
