using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin.Actors
{
    /// <summary>
    /// class to convert a SharePoint location and its children to SPLocation objects
    /// </summary>
    public class LocationActor : ReceiveActor
    {
        private readonly IDataService dataService;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public LocationActor(IDataService dataService)
        {
            this.dataService = dataService;

            Receive<LoadLocationQuery>(message => HandleLoadLocationQuery(message));
            Receive<FeatureToggleRequest>(message => HandleFeatureToggleRequest(message));
        }

        /// <summary>
        /// at this time, there is always only one feature to be activated or deactivated in a location with same scope
        /// </summary>
        /// <param name="message"></param>
        private void HandleFeatureToggleRequest(FeatureToggleRequest message)
        {
            _log.Debug("Entered LocationActor-HandleFeatureToggleRequest");

            if (message.Activate)
            {
                ActivatedFeature af;
                var error = dataService.ActivateFeature(
                    message.FeatureDefinition
                    , message.Location
                    , message.ElevatedPrivileges
                    , message.Force
                    , out af);

                var completed = new Core.Messages.Completed.FeatureActivationCompleted(
               message.TaskId
               , message.Location.Id
               , af
               , string.Empty
               );

                Sender.Tell(completed);

            }
            else
            {
                var error = dataService.DeactivateFeature(
                    message.FeatureDefinition
                    , message.Location
                    , message.ElevatedPrivileges
                    , message.Force);

                var completed = new Core.Messages.Completed.FeatureDeactivationCompleted(
                               message.TaskId
                               , message.Location.Id
                               , message.FeatureDefinition.Id
                               , error
                               );

                Sender.Tell(completed);
            }
        }

        private void HandleLoadLocationQuery(LoadLocationQuery message)
        {
            _log.Debug("Entered LocationActor-LookupLocationHandlyLoadLocationQuery");

            var location = message.Location;

            if (location.Scope == Core.Models.Enums.Scope.Farm)
            {
                Sender.Tell(dataService.LoadFarmAndWebApps());
            }
            else
            {
                Sender.Tell(dataService.LoadNonFarmLocationAndChildren(location));
            }
        }

        /// <summary>
        /// Props provider
        /// </summary>
        /// <param name="dataService">SharePoint data service</param>
        /// <returns></returns>
        /// <remarks>
        /// see also https://getakka.net/articles/actors/receive-actor-api.html
        /// </remarks>
        public static Props Props(IDataService dataService)
        {
            return Akka.Actor.Props.Create(() => new LocationActor(
                   dataService));
        }
    }
}
