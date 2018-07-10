using Akka.Actor;
using Akka.Event;
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

            Receive<LoadChildLocationQuery>(message => ReceiveLoadChildLocationQuery(message));
            Receive<FeatureToggleRequest>(message => ReceiveFeatureToggleRequest(message));
        }

        /// <summary>
        /// at this time, there is always only one feature to be activated or deactivated in a location with same scope
        /// </summary>
        /// <param name="message"></param>
        private void ReceiveFeatureToggleRequest(FeatureToggleRequest message)
        {
            _log.Debug("Entered LocationActor-HandleFeatureToggleRequest");

            string errorMsg = null;

            if (message.Activate)
            {
                ActivatedFeature af;

                switch (message.Location.Scope)
                {
                    case Core.Models.Enums.Scope.Web:
                        errorMsg += dataService.ActivateWebFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.ElevatedPrivileges.Value,
                            message.Force.Value,
                            out af);
                        break;
                    case Core.Models.Enums.Scope.Site:
                        errorMsg += dataService.ActivateSiteFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.ElevatedPrivileges.Value,
                            message.Force.Value,
                            out af);
                        break;
                    case Core.Models.Enums.Scope.WebApplication:
                        errorMsg += dataService.ActivateWebAppFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.Force.Value,
                            out af);
                        break;
                    case Core.Models.Enums.Scope.Farm:
                        errorMsg += dataService.ActivateFarmFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.Force.Value,
                            out af);
                        break;
                    case Core.Models.Enums.Scope.ScopeInvalid:
                        errorMsg += string.Format("Location '{0}' has invalid scope - not supported for feature activation.", message.Location.Id);
                        af = null;
                        break;
                    default:
                        errorMsg += string.Format("Location '{0}' has unidentified scope - not supported for feature activation.", message.Location.Id);
                        af = null;
                        break;
                }

                var completed = new Core.Messages.Completed.FeatureActivationCompleted(
               message.TaskId,
               message.Location.Id,
               af,
               errorMsg
               );

                Sender.Tell(completed);

            }
            else
            {
                switch (message.Location.Scope)
                {
                    case Core.Models.Enums.Scope.Web:
                        errorMsg += dataService.DeactivateWebFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.ElevatedPrivileges.Value,
                            message.Force.Value);
                        break;
                    case Core.Models.Enums.Scope.Site:
                        errorMsg += dataService.DeactivateSiteFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.ElevatedPrivileges.Value,
                            message.Force.Value
                            );
                        break;
                    case Core.Models.Enums.Scope.WebApplication:
                        errorMsg += dataService.DeactivateWebAppFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.Force.Value
                            );
                        break;
                    case Core.Models.Enums.Scope.Farm:
                        errorMsg += dataService.DeactivateFarmFeature(
                            message.FeatureDefinition,
                            message.Location,
                            message.Force.Value
                            );
                        break;
                    case Core.Models.Enums.Scope.ScopeInvalid:
                        errorMsg += string.Format("Location '{0}' has invalid scope - not supported for feature deactivation.", message.Location.Id);
                        break;
                    default:
                        errorMsg += string.Format("Location '{0}' has unidentified scope - not supported for feature deactivation.", message.Location.Id);
                        break;
                }

                var completed = new Core.Messages.Completed.FeatureDeactivationCompleted(
                               message.TaskId
                               , message.Location.Id
                               , message.FeatureDefinition.Id
                               , errorMsg
                               );

                Sender.Tell(completed);
            }
        }

        private void ReceiveLoadChildLocationQuery(LoadChildLocationQuery message)
        {
            _log.Debug("Entered LocationActor-LookupLocationHandlyLoadLocationQuery");

            var location = message.Location;

            if (location == null)
            {
                Sender.Tell(dataService.LoadFarm());
            }
            else
            {
                if (location.Scope == Core.Models.Enums.Scope.Farm)
                {
                    Sender.Tell(dataService.LoadWebApps());
                }
                else
                {
                    Sender.Tell(dataService.LoadWebAppChildren(location,message.ElevatedPrivileges));
                }
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
