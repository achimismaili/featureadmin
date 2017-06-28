using Microsoft.SharePoint;
using System.Linq;
using Xunit;

namespace FeatureAdmin.Test
{
    /// <summary>
    /// For SharePoint tests, make sure, you run in 64bit mode - Test - Test Settings - Default Processor Architecture - X64
    /// </summary>
    public class ActivationFinderTest
    {
        [Fact]
        public void FindAllActivationsOfAllFeatures()
        {
            // Arrange
            FeatureAdmin.ActivationFinder finder = new FeatureAdmin.ActivationFinder();
            // No Found callback b/c we process final list

            // Act
            var featureList = finder.FindAllActivationsOfAllFeatures();


            // Assert
            int faultyWeb = featureList[TestContent.TestFeatures.FaultyWeb.Id].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.FaultyWebFeatureActivatedTotal, faultyWeb);

            int faultySiCo = featureList[TestContent.TestFeatures.FaultySite.Id].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.FaultySiCoFeatureActivatedTotal, faultySiCo);

            // faulty web app feature gets removed on retract ...
            // int faultyWebApp = featureList[TestContent.TestFeatures.FaultyWebApp].Count();
            // Assert.Equal(TestContent.SharePointContainers.Farm.FaultyWebAppFeatureActivatedTotal, faultyWebApp);

            // faulty farm feature gets removed on retract ...
            //int faultyFarm = featureList[TestContent.TestFeatures.FaultyFarm].Count();
            //Assert.Equal(TestContent.SharePointContainers.Farm.FaultyFarmFeatureActivated, faultyFarm);

            int healthyWeb = featureList[TestContent.TestFeatures.HealthyWeb.Id].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyWebFeatureActivatedTotal, healthyWeb);

            int healthySiCo = featureList[TestContent.TestFeatures.HealthySite.Id].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthySiCoFeatureActivatedTotal, healthySiCo);

            int healthyWebApp = featureList[TestContent.TestFeatures.HealthyWebApp.Id].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyWebAppFeatureActivatedTotal, healthyWebApp);

            int healthyFarm = featureList[TestContent.TestFeatures.HealthyFarm.Id].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyFarmFeatureActivated, healthyFarm);
        }
    }
}
