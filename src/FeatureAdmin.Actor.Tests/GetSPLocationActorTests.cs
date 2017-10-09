using Akka.TestKit.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FeatureAdmin.Actor.Actors;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using Akka.Actor;
using FeatureAdmin.Backends.Services;

namespace FeatureAdmin.Actor.Tests
{
    public class GetSPLocationActorTests : TestKit
    {
        private DemoDataService dataService;
        private LoadActor actor;
        public GetSPLocationActorTests()
        {
            dataService = new DemoDataService();
            actor = new LoadActor(dataService);
        }

        [Fact]
        public void ShouldHandleGetLocation()
        {
            var undefinedLocation = Location.GetLocationUndefined(Guid.Empty, Guid.Empty);
            actor.LoadLocation(undefinedLocation);

            Assert.Equal(Core.Models.Enums.Scope.ScopeInvalid, actor.Location.Scope);
        }

        //[Fact]
        //public void ShouldReceiveInitialLocationMessage()
        //{
        //    IActorRef actor = ActorOf<LoadActor>(dataService);

        //    var undefinedLocation = Location.GetLocationUndefined(Guid.Empty, Guid.Empty);
        //    actor.GetLocation(undefinedLocation);

        //    Assert.Equal(Core.Models.Enums.Scope.ScopeInvalid, actor.SPLocation.Scope);
        //}
    }
}
