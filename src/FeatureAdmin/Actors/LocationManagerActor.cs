using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using FeatureAdmin.Core.Messages;
using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Actors
{
    public class LocationManagerActor : ReceiveActor
    {
        private Guid myLocation;
        private readonly IActorRef _locationActorChild;
        private readonly IActorRef viewModelSyncActor;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public LocationManagerActor(IActorRef viewModelSyncActor, Guid myLocation)
        {
            _locationActorChild = Context.ActorOf(Context.DI().Props<LocationActor>());

            this.myLocation = myLocation;

            this.viewModelSyncActor = viewModelSyncActor;

            Receive<LoadLocationQuery>(message => LoadLocation(message));
            Receive<ItemUpdated<Location>>(message => LocationUpdated(message));
            Receive<ItemUpdated<IEnumerable<Location>>>(message => LocationsUpdated(message));
        }

        private void LoadLocation(LoadLocationQuery message)
        {
            _log.Debug("Entered LocationManager-LoadLocation");

            _locationActorChild.Tell(message);
        }

        private void LocationsUpdated(ItemUpdated<IEnumerable<Location>> message)
        {
            if (message == null || message.Item == null)
            {
                _log.Error("empty LocationsUpdated message returned!");
                return;
            }

            if (message.ReportToTaskManager)
            {
                var synclocations = new List<Location>();
                // send web apps to task manager and farm to sync actor

                foreach (Location l in message.Item)
                {
                    if (l.Scope == Core.Models.Enums.Scope.WebApplication)
                    {
                        // report other web applications to task manager to get it processed by different actor
                        Context.Parent.Tell(new ItemUpdated<Location>(l));
                    }
                    else
                    {
                        synclocations.Add(l);
                    }
                }

                viewModelSyncActor.Tell(new ItemUpdated<IEnumerable<Location>>(synclocations));
            }
            else
            {
                viewModelSyncActor.Tell(message);
            }
        }

        /// <summary>
        /// Only for Farms and Web Applications
        /// </summary>
        /// <param name="message"></param>
        private void LocationUpdated(ItemUpdated<Location> message)
        {
            if (message == null || message.Item == null)
            {
                _log.Error("empty LocationUpdated message returned!");
                return;
            }

            // because all web apps are loaded as itemupdated with ienumerable, 
            // and only farm loads with single itemupdated locations
            // it is no longer possible, that scope is web app and web app id is same as mylocation:
            // commented out
            // if (message.Item.Scope == Core.Models.Enums.Scope.WebApplication && message.Item.Id != myLocation)


            else
            {
                // report loaded location to viewmodelsync actor , if this is mylocation or if this location cannot have children
                viewModelSyncActor.Tell(message);
            }
        }
    }
}
