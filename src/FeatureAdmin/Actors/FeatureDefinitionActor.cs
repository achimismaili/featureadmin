using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.Actors
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
                Sender.Tell(new Core.Messages.Tasks.FarmFeatureDefinitionsLoaded(
                    message.TaskId,
                    farmFeatureDefinitions));

            //TODO: maybe check, if feature definitions from here have more data, than from activated features ...
        }
    }
}
