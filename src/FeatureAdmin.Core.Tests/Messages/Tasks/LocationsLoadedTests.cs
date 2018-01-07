using FeatureAdmin.Core.Factories;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Tests.Common;
using FeatureAdmin.SampleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FeatureAdmin.Core.Tests.Common.TestData;

namespace FeatureAdmin.Core.Tests.Messages.Tasks
{
    public class LocationsLoadedTests
    {

        [Fact]
        public void ActivatedFeaturesAreGroupedCorrectly()
        {
            // Arrange

            // get 3 locations with each 4 same activated features
            Guid[] lIds = { TestLocations.Ids.Id001,
                TestLocations.Ids.Id002,
                TestLocations.Ids.Id003 };

            Guid[] fIds = { TestFeatureDefinitions.Ids.Id004 ,
                    TestFeatureDefinitions.Ids.Id001,
                    TestFeatureDefinitions.Ids.Id002,
                    TestFeatureDefinitions.Ids.Id003 };

            var locations = TestLocations.GetLocations(lIds, fIds); 

            // Act

            var msg = new Core.Messages.Tasks.LocationsLoaded(Guid.NewGuid(),null, locations);

            // Assert
            // 4 different feature definitions
            var differentFeatureDefinitions = 4;
            Assert.Equal(differentFeatureDefinitions, msg.LoadedFeatures.Count());

            foreach (var featureDefinitionGroup in msg.LoadedFeatures)
            {
                var activeFeaturesPerDefinition = 3;
                Assert.Equal(activeFeaturesPerDefinition, featureDefinitionGroup.Count());
            }
        }

    }
}

