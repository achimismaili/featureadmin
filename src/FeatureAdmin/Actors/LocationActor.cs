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
    public class LocationActor: ReceiveActor
        {
            private readonly IDataService dataService;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public LocationActor(IDataService dataService)
            {
                this.dataService = dataService;

                Receive<LoadLocationQuery>(message => LookupLocation(message));
            }

            private void LookupLocation(LoadLocationQuery message)
            {
            // first, generate Location from SharePoint object
            _log.Debug("Entered LocationActor-LookupLocation");

            if (message == null || message.SPLocation == null)
            {
                _log.Error("LookupLocation: message or message.splocation was null");
            }

            SPLocation location = message.SPLocation;

            if (location.RequiresUpdate || location.SPLocationObject == null)
            {
                // in SharePoint Backend, the real SPWeb, SPSite, web app or farm would have to be reloaded ...
                location = dataService.LoadLocation(message.SPLocation);
            }

            Sender.Tell(new ItemUpdated<SPLocation>(location));

            if (location.CanHaveChildren)
            {
                // get child locations
                var children = dataService.LoadChildLocations(location);

                if (children != null)
                {
                    foreach (SPLocation l in children)
                    {
                        Sender.Tell(new ItemUpdated<SPLocation>(l));
                    }
                }
            }
           




        }
        }
    }
