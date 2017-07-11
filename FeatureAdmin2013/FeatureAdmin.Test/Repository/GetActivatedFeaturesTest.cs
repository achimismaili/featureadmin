using FeatureAdminForm.Services;
using Microsoft.SharePoint;
using System.Linq;
using Xunit;

namespace FeatureAdmin.Test.Repository
{
    /// <summary>
    /// For SharePoint tests, make sure, you run in 64bit mode - Test - Test Settings - Default Processor Architecture - X64
    /// </summary>
    public class GetActivatedFeaturesTEst
    {
        private FeatureRepository repository;

        public GetActivatedFeaturesTEst()
        {
            // Arrange
            repository = new FeatureRepository();
        }

        [Fact]
        public void FindAllActivationsOfAllFeatures()
        {
            // No Found callback b/c we process final list

            // Act
            var featureList = repository.GetActivatedFeatures ();


            // Assert
            int faultyWeb = featureList.Where(f => f.Id == TestContent.TestFeatures.FaultyWeb.Id).Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.FaultyWebFeatureActivatedTotal, faultyWeb);

            int faultySiCo = featureList.Where(f => f.Id == TestContent.TestFeatures.FaultySite.Id).Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.FaultySiCoFeatureActivatedTotal, faultySiCo);

            // faulty web app feature gets removed on retract ...
            // int faultyWebApp = featureList[TestContent.TestFeatures.FaultyWebApp].Count();
            // Assert.Equal(TestContent.SharePointContainers.Farm.FaultyWebAppFeatureActivatedTotal, faultyWebApp);

            // faulty farm feature gets removed on retract ...
            //int faultyFarm = featureList[TestContent.TestFeatures.FaultyFarm].Count();
            //Assert.Equal(TestContent.SharePointContainers.Farm.FaultyFarmFeatureActivated, faultyFarm);

            int healthyWeb = featureList.Where(f => f.Id == TestContent.TestFeatures.HealthyWeb.Id).Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyWebFeatureActivatedTotal, healthyWeb);

            int healthySiCo = featureList.Where(f => f.Id == TestContent.TestFeatures.HealthySite.Id).Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthySiCoFeatureActivatedTotal, healthySiCo);

            int healthyWebApp = featureList.Where(f => f.Id == TestContent.TestFeatures.HealthyWebApp.Id).Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyWebAppFeatureActivatedTotal, healthyWebApp);

            int healthyFarm = featureList.Where(f => f.Id == TestContent.TestFeatures.HealthyFarm.Id).Count();
            Assert.Equal(TestContent.SharePointContainers.Farm.HealthyFarmFeatureActivated, healthyFarm);
        }
    }
}
