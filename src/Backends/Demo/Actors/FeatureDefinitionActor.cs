using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Actors
{
    /// <summary>
    /// class to convert a SharePoint location and its children to SPLocation objects
    /// </summary>
    public class FeatureDefinitionActor : ReceiveActor
    {
        private readonly IDataService dataService;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public FeatureDefinitionActor(IDataService dataService)
        {
            this.dataService = dataService;

            Receive<LoadFeatureDefinitionQuery>(message => LoadFarmFeatures(message));
        }

        private void LoadFarmFeatures(LoadFeatureDefinitionQuery message)
        {

            _log.Debug("Entered LoadFarmFeatures");

            var farmFeatureDefinitions = dataService.LoadFarmFeatureDefinitions();

            if (farmFeatureDefinitions == null)
            {
                _log.Error("Farm Feature Definitions not found!");
            }

            foreach (FeatureDefinition fd in farmFeatureDefinitions)
            {

                Sender.Tell(new FeatureDefinitionUpdated(fd));
            }
        }
    }
}
