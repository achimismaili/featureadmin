using FeatureAdmin.Models;
using FeatureAdmin.Repository;
using FeatureAdmin.Services.SharePointApi;
using FeatureAdmin.Test.TestContent.MockModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using Xunit;

namespace FeatureAdmin.Test.ServicesSharePointApiTest
{
    public class FeatureActivationAndDeactivationTest : IDisposable
    {
        private const string appConfigKeyPowerShellScriptPath = "PowerShellScriptPath";

        [Fact]
        public void CanDeactivateAndActivateHealthyFeatures()
        {
            // Arrange
            int processingCounter = 0;

            Exception exception;

            // Act
            processingCounter = FeatureActivationAndDeactivationBulk.DeactivateAllFeatures(TestContent.TestFeatures.AllHealthyActivatedFeatures, false, out exception);

            // Assert
            Assert.Equal(6, processingCounter);

            // Act - activate healthy web and sico feature in active sico
            processingCounter = FeatureActivationAndDeactivationBulk.ActivateAllFeaturesWithinSharePointContainer(
                TestContent.SharePointContainers.SiCoActivated.GetMockFeatureParent(),
                TestContent.TestFeatures.AllHealthyFeatureDefinitions,
                false, 
                out exception);

            // after this, also the feature in inactive sub web is active ... :(
            Assert.Equal(4, processingCounter);

            // Act - activate healthy web feature in inactive sico subweb active
            processingCounter = FeatureActivationAndDeactivationBulk.ActivateAllFeaturesWithinSharePointContainer(
                TestContent.SharePointContainers.SiCoInActive.SubWebActivated.GetMockFeatureParent(),
                TestContent.TestFeatures.AllHealthyFeatureDefinitions,
                false,
                out exception);

            // Assert
            Assert.Equal(1, processingCounter);

            // from here more or less do tests and rollback previous activation state ...

            // Act - deactivate the healthy web feature in inactive sico subweb inactive
            processingCounter = FeatureActivationAndDeactivationBulk.DeactivateAllFeatures(
                new List<MockActivatedFeature>() {
                TestContent.TestFeatures.HealthyWeb.GetMockActivatedFeature(TestContent.SharePointContainers.SiCoActivated.SubWebInactive.GetMockFeatureParent())
                },
                false,
                out exception);

            // Assert
            Assert.Equal(1, processingCounter);

            // Act activate web app feature
            processingCounter = FeatureActivationAndDeactivationCore.ProcessWebAppFeatureInWebApp(
                FarmRead.GetWebAppByUrl(TestContent.SharePointContainers.WebApplication.Url),
                TestContent.TestFeatures.HealthyWebApp.Id,
                true,
                false,
                out exception);

            // Assert
            Assert.Equal(1, processingCounter);

            // Act activate farm feature
            processingCounter = FeatureActivationAndDeactivationCore.ProcessFarmFeatureInFarm(
                FarmRead.GetFarm(),
                TestContent.TestFeatures.HealthyFarm.Id,
                true,
                false,
                out exception);

            // Assert
            Assert.Equal(1, processingCounter);
        }

        [Fact]
        /// <summary>
        /// warning, after this test, the reference content needs to be re-set up
        /// </summary>
        public void CanDeactivateFaultyWebFeatures()
        {
            // Arrange
            int processingCounter = 0;

            Exception exception;

            // Act
            processingCounter = FeatureActivationAndDeactivationBulk.DeactivateAllFeatures(
                //new List<MockActivatedFeature>() {
                //    TestContent.TestFeatures.FaultyWeb.GetMockActivatedFeatureInSiCoActivatedRootWeb() }, 
                TestContent.TestFeatures.AllFaultyActivatedFeatures,
                true, 
                out exception);

            // Assert
            Assert.Equal(4, processingCounter);
        }

        public void Dispose()
        {
            var scriptFullPath = System.Configuration.ConfigurationManager.AppSettings[appConfigKeyPowerShellScriptPath];
          
            CommandParameter param = new CommandParameter(TestContent.SharePointContainers.WebApplication.Url);

            var parameters = new List<CommandParameter>();
            parameters.Add(param);
            Helpers.ExecutePowerShell.RunScript(scriptFullPath, parameters);
        }
    }
}
