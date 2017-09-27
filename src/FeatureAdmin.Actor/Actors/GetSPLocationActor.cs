using Akka.Actor;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.SharePointFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor.Actors
{
    public class GetSPLocationActor : ReceiveActor
    {
        private ISharePointLocationFactory locationFactory;

        public SPObject SPLocation { get; private set; }

        public GetSPLocationActor(ISharePointLocationFactory locationFactory)
        {
            this.locationFactory = locationFactory;
            Receive<Location>(message => GetLocation(message));
        }

        public void GetLocation(Location message)
        {
            SPLocation = locationFactory.GetLocation(message);
        }
    }
}
