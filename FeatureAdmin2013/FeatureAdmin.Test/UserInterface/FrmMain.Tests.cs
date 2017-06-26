using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeatureAdmin.Test.UserInterface
{
    public class FrmMainTests
    {

        // The following buttons should be tested:

        // Load
        [Fact]
        public void LoadButtonTest()
        {
            // click: btnLoadFDefs_Click

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
        // Reload Web Apps

        // Review Activations of Selected Feature

        // Activate / Deactivate in selected Web

        // Activate / Deactivate across selected SiCo

        // Activate / Deactivate across selected Web App

        // Activate / Deactivate across selected Farm

        // Uninstall Feature Definition

        // Find Faulty Feature in Farm


        // Clear Log (priority low)

    }
}
