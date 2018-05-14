using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FeatureAdmin.Core.Tests.Common.TestData;
using FeatureAdmin.Core.Factories;

namespace FeatureAdmin.Core.Tests.Factories
{
    public class FeatureDefinitionFactoryTests
    {
        protected LocationsLoaded Loaded3LocationsWithFirst4ActivatedFeaturesEach;

        public FeatureDefinitionFactoryTests()
        {
            // get 3 locations with each 4 same activated features
            Guid[] lIds = { TestLocations.Ids.Id001,
                TestLocations.Ids.Id002,
                TestLocations.Ids.Id003 };

            Guid[] fIds = { TestFeatureDefinitions.Ids.Id004 ,
                    TestFeatureDefinitions.Ids.Id001,
                    TestFeatureDefinitions.Ids.Id002,
                    TestFeatureDefinitions.Ids.Id003 };

            var locations = TestLocations.GetLocations(lIds, fIds);

            Loaded3LocationsWithFirst4ActivatedFeaturesEach = new LocationsLoaded(Guid.NewGuid(), null, locations, null, null);
        }


        [Fact]
        public void FactoryAddActivatedFeaturesDoesNotAddExistingDefinitions()
        {
            // Arrange
            Guid[] fIds = { TestFeatureDefinitions.Ids.Id004 ,
                    TestFeatureDefinitions.Ids.Id001,
                    TestFeatureDefinitions.Ids.Id002,
                    TestFeatureDefinitions.Ids.Id003 };

            Guid[] lIds = { TestLocations.Ids.Id005,
                TestLocations.Ids.Id006,
                TestLocations.Ids.Id007 };

            var featureDefinitions = TestFeatureDefinitions.GetFeatureDefinitions(fIds, lIds).ToList();

            // Act

            featureDefinitions.AddActivatedFeatures(Loaded3LocationsWithFirst4ActivatedFeaturesEach.ActivatedFeatures);

            // Assert

            var expectedFeatureDefinitions = 4;
            Assert.Equal(expectedFeatureDefinitions, featureDefinitions.Count());

            foreach (Guid featureId in fIds)
            {
                var definition = featureDefinitions.FirstOrDefault(fd => fd.Id == featureId);

                var expectedActivatedFeatures = 6;

                Assert.Equal(expectedActivatedFeatures, definition.ActivatedFeatures.Count());
            }

        }

        [Fact]
        public void FactoryAddActivatedFeaturesCanAddDefinitionsIfItDoesNotExist()
        {
            // Arrange
            Guid[] fIds = { TestFeatureDefinitions.Ids.Id008 ,
                    TestFeatureDefinitions.Ids.Id009,
                    TestFeatureDefinitions.Ids.Id010 };

            Guid[] lIds = { TestLocations.Ids.Id005,
                TestLocations.Ids.Id008,
                TestLocations.Ids.Id009 };

            var featureDefinitions = TestFeatureDefinitions.GetFeatureDefinitions(fIds, lIds).ToList();

            // Act

            featureDefinitions.AddActivatedFeatures(Loaded3LocationsWithFirst4ActivatedFeaturesEach.ActivatedFeatures);

            // Assert

            var expectedFeatureDefinitions = 7;
            Assert.Equal(expectedFeatureDefinitions, featureDefinitions.Count());

            foreach (Guid featureId in fIds)
            {
                var definition = featureDefinitions.FirstOrDefault(fd => fd.Id == featureId);

                var expectedActivatedFeatures = 3;

                Assert.Equal(expectedActivatedFeatures, definition.ActivatedFeatures.Count());
            }

        }
    }
}
