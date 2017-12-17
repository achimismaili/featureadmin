using Akka.Actor;
using Akka.Event;
using FeatureAdmin.Core;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using System.Collections.Generic;

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

            Receive<LoadLocationQuery>(message => LookupLocation(message));
        }

        private void LookupLocation([NotNull] LoadLocationQuery message)
        {
            // first, generate Location from SharePoint object
            _log.Debug("Entered LocationActor-LookupLocation");

            var locations = new List<Location>();

            var location = message.Location;

            if (location.Scope == Core.Models.Enums.Scope.Farm)
            {
                locations.AddRange(dataService.LoadFarmAndWebApps());
            }
            else
            {
                locations.AddRange(dataService.LoadNonFarmLocationAndChildren(location));
            }

            Sender.Tell(new ItemUpdated<IEnumerable<Location>>(
                                message.TaskId,
                                locations));
        }
    }
}
