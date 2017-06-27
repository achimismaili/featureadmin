using Microsoft.SharePoint;
using System.Linq;
using Xunit;

namespace FeatureAdmin.Test
{
    public class ActivationFinder
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
            int faultyWeb = featureList[TestContent.TestFeatures.featureIdFaultyWeb].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.FaultyWebFeatureActivatedTotal, faultyWeb);

            int faultySiCo = featureList[TestContent.TestFeatures.featureIdFaultySiCo].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.FaultySiCoFeatureActivatedTotal, faultySiCo);

            // faulty web app feature gets removed on retract ...
            // int faultyWebApp = featureList[TestContent.TestFeatures.featureIdFaultyWebApp].Count();
            // Assert.Equal(TestContent.SharePointContainers.Farm.FaultyWebAppFeatureActivatedTotal, faultyWebApp);

            // faulty farm feature gets removed on retract ...
            //int faultyFarm = featureList[TestContent.TestFeatures.featureIdFaultyFarm].Count();
            //Assert.Equal(TestContent.SharePointContainers.Farm.FaultyFarmFeatureActivated, faultyFarm);

            int healthyWeb = featureList[TestContent.TestFeatures.featureIdHealthyWeb].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyWebFeatureActivatedTotal, healthyWeb);

            int healthySiCo = featureList[TestContent.TestFeatures.featureIdHealthySiCo].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthySiCoFeatureActivatedTotal, healthySiCo);

            int healthyWebApp = featureList[TestContent.TestFeatures.featureIdHealthyWebApp].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyWebAppFeatureActivatedTotal, healthyWebApp);

            int healthyFarm = featureList[TestContent.TestFeatures.featureIdHealthyFarm].Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyFarmFeatureActivated, healthyFarm);
        }
    }
}
