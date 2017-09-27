using Akka.TestKit.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using FeatureAdmin.Actor.Actors;
using FeatureAdmin.Core.SharePointFactories;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Actor.Tests
{
    public class GetSPLocationActorTests : TestKit
    {
        private GetSPLocationActor actor;
        public GetSPLocationActorTests()
        {
            var splf = new Backends.Demo.Factories.SharePointLocationFactory();
            actor = new GetSPLocationActor(splf);
        }

        [Fact]
        public void ShouldHandleGetLocation()
        {
            var undefinedLocation = Location.GetLocationUndefined(Guid.Empty, Guid.Empty);
            actor.GetLocation(undefinedLocation);

            Assert.Equal(Scope.ScopeInvalid, actor.SPLocation.Scope);
        }
    }
}
