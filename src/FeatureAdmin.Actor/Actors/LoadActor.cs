using Akka.Actor;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Services;
using System.Collections.Generic;

namespace FeatureAdmin.Actor.Actors
{
    public class LoadActor : ReceiveActor
    {
        private IDataService dataService;

        public Location Location { get; private set; }
        public LoadActor(IDataService dataService)
        {
            this.dataService = dataService;
            Receive<Location>(message => GetLocation(message));
        }

        public void GetLocation(Location message)
        {
            Location = dataService.ReLoadLocation(message);
        }
    }
}
