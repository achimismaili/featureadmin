using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Messages.Request;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using System.Collections.Generic;
using System;

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

            Receive<LoadLocationQuery>(message => HandlyLoadLocationQuery(message));
            Receive<FeatureToggleRequest>(message => HandleFeatureToggleRequest(message));
        }

        /// <summary>
        /// at this time, there is always only one feature to be activated or deactivated in a location with same scope
        /// </summary>
        /// <param name="message"></param>
        private void HandleFeatureToggleRequest(FeatureToggleRequest message)
        {
            _log.Debug("Entered LocationActor-HandleFeatureToggleRequest");

            dataService.FeatureToggle(message.Location, message.FeatureDefinition, message.Activate, message.Force, message.ElevatedPrivileges);
        }

        private void HandlyLoadLocationQuery([NotNull] LoadLocationQuery message)
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
    }
}
