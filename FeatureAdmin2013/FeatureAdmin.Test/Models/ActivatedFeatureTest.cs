using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Linq;
using Xunit;

namespace FeatureAdmin.Test.Models
{
    /// <summary>
    /// For SharePoint tests, make sure, you run in 64bit mode - Test - Test Settings - Default Processor Architecture - X64
    /// </summary>
    public class ActivatedFeatureTest
    {
        [Fact]
        public void CanGetActivatedFeatureFromWeb()
        {
            // Arrange
            var parentScope = SPFeatureScope.Web;
            var parentUrl = TestContent.SharePointContainers.SiCoActivated.Url;

            using (SPSite site = new SPSite(parentUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    var parent = web;

                    var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthyWeb.Id];
                    var testFeatureFaulty = parent.Features[TestContent.TestFeatures.FaultyWeb.Id];

                    var featureParent = new FeatureParent(parent.Title, parentUrl, parent.ID, parentScope);

                    // Act
                    var sharePointFeatureHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy);
                    var sharePointFeatureFaulty = ActivatedFeature.GetActivatedFeature(testFeatureFaulty);

                    var sharePointFeatureRetrievedWithParentHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy, featureParent);
                    var sharePointFeatureRetrievedWithParentFaulty = ActivatedFeature.GetActivatedFeature(testFeatureFaulty, featureParent);

                    // Assert
                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Id, sharePointFeatureFaulty.Id);
                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Name, sharePointFeatureFaulty.Name);
                    Assert.Equal(featureParent.Url, sharePointFeatureFaulty.Parent.Url);
                    Assert.Equal(featureParent.DisplayName, sharePointFeatureFaulty.Parent.DisplayName);
                    Assert.Equal(featureParent.Id, sharePointFeatureFaulty.Parent.Id);
                    Assert.Equal(featureParent.Scope, sharePointFeatureFaulty.Parent.Scope);
                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Version, sharePointFeatureFaulty.Version);
                    Assert.Equal(parentScope, sharePointFeatureFaulty.Scope);
                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Faulty, sharePointFeatureFaulty.Faulty);

                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Id, sharePointFeatureRetrievedWithParentFaulty.Id);
                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Name, sharePointFeatureRetrievedWithParentFaulty.Name);
                    Assert.Equal(featureParent.Url, sharePointFeatureRetrievedWithParentFaulty.Parent.Url);
                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Version, sharePointFeatureRetrievedWithParentFaulty.Version);
                    Assert.Equal(parentScope, sharePointFeatureRetrievedWithParentFaulty.Scope);
                    Assert.Equal(TestContent.TestFeatures.FaultyWeb.Faulty, sharePointFeatureRetrievedWithParentFaulty.Faulty);

                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Id, sharePointFeatureHealthy.Id);
                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Name, sharePointFeatureHealthy.Name);
                    Assert.Equal(featureParent.Url, sharePointFeatureHealthy.Parent.Url);
                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Version, sharePointFeatureHealthy.Version);
                    Assert.Equal(parentScope, sharePointFeatureHealthy.Scope);
                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Faulty, sharePointFeatureHealthy.Faulty);

                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Id, sharePointFeatureRetrievedWithParentHealthy.Id);
                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Name, sharePointFeatureRetrievedWithParentHealthy.Name);
                    Assert.Equal(featureParent.Url, sharePointFeatureRetrievedWithParentHealthy.Parent.Url);
                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Version, sharePointFeatureRetrievedWithParentHealthy.Version);
                    Assert.Equal(parentScope, sharePointFeatureRetrievedWithParentHealthy.Scope);
                    Assert.Equal(TestContent.TestFeatures.HealthyWeb.Faulty, sharePointFeatureRetrievedWithParentHealthy.Faulty);
                }
            }
        }

        [Fact]
        public void CanGetActivatedFeatureFromSite()
        {
            // Arrange
            var parentScope = SPFeatureScope.Site;
            var parentUrl = TestContent.SharePointContainers.SiCoActivated.Url;

            using (SPSite site = new SPSite(parentUrl))
            {
                var parent = site;

                var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthySite.Id];
                var testFeatureFaulty = parent.Features[TestContent.TestFeatures.FaultySite.Id];

                var featureParent = new FeatureParent(parent.RootWeb.Title, parentUrl, parent.ID, parentScope);

                // Act
                var sharePointFeatureHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy);
                var sharePointFeatureFaulty = ActivatedFeature.GetActivatedFeature(testFeatureFaulty);

                var sharePointFeatureRetrievedWithParentHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy, featureParent);
                var sharePointFeatureRetrievedWithParentFaulty = ActivatedFeature.GetActivatedFeature(testFeatureFaulty, featureParent);

                // Assert
                Assert.Equal(TestContent.TestFeatures.FaultySite.Id, sharePointFeatureFaulty.Id);
                Assert.Equal(TestContent.TestFeatures.FaultySite.Name, sharePointFeatureFaulty.Name);
                Assert.Equal(featureParent.Url, sharePointFeatureFaulty.Parent.Url);
                Assert.Equal(featureParent.DisplayName, sharePointFeatureFaulty.Parent.DisplayName);
                Assert.Equal(featureParent.Id, sharePointFeatureFaulty.Parent.Id);
                Assert.Equal(featureParent.Scope, sharePointFeatureFaulty.Parent.Scope);
                Assert.Equal(TestContent.TestFeatures.FaultySite.Version, sharePointFeatureFaulty.Version);
                Assert.Equal(parentScope, sharePointFeatureFaulty.Scope);
                Assert.Equal(TestContent.TestFeatures.FaultySite.Faulty, sharePointFeatureFaulty.Faulty);

                Assert.Equal(TestContent.TestFeatures.FaultySite.Id, sharePointFeatureRetrievedWithParentFaulty.Id);
                Assert.Equal(TestContent.TestFeatures.FaultySite.Name, sharePointFeatureRetrievedWithParentFaulty.Name);
                Assert.Equal(featureParent.Url, sharePointFeatureRetrievedWithParentFaulty.Parent.Url);
                Assert.Equal(TestContent.TestFeatures.FaultySite.Version, sharePointFeatureRetrievedWithParentFaulty.Version);
                Assert.Equal(parentScope, sharePointFeatureRetrievedWithParentFaulty.Scope);
                Assert.Equal(TestContent.TestFeatures.FaultySite.Faulty, sharePointFeatureRetrievedWithParentFaulty.Faulty);

                Assert.Equal(TestContent.TestFeatures.HealthySite.Id, sharePointFeatureHealthy.Id);
                Assert.Equal(TestContent.TestFeatures.HealthySite.Name, sharePointFeatureHealthy.Name);
                Assert.Equal(featureParent.Url, sharePointFeatureHealthy.Parent.Url);
                Assert.Equal(TestContent.TestFeatures.HealthySite.Version, sharePointFeatureHealthy.Version);
                Assert.Equal(parentScope, sharePointFeatureHealthy.Scope);
                Assert.Equal(TestContent.TestFeatures.HealthySite.Faulty, sharePointFeatureHealthy.Faulty);

                Assert.Equal(TestContent.TestFeatures.HealthySite.Id, sharePointFeatureRetrievedWithParentHealthy.Id);
                Assert.Equal(TestContent.TestFeatures.HealthySite.Name, sharePointFeatureRetrievedWithParentHealthy.Name);
                Assert.Equal(featureParent.Url, sharePointFeatureRetrievedWithParentHealthy.Parent.Url);
                Assert.Equal(TestContent.TestFeatures.HealthySite.Version, sharePointFeatureRetrievedWithParentHealthy.Version);
                Assert.Equal(parentScope, sharePointFeatureRetrievedWithParentHealthy.Scope);
                Assert.Equal(TestContent.TestFeatures.HealthySite.Faulty, sharePointFeatureRetrievedWithParentHealthy.Faulty);
            }
        }

        [Fact]
        public void CanGetActivatedFeatureFromWebApp()
        {
            // Arrange
            var parentScope = SPFeatureScope.WebApplication;
            var parentUrl = TestContent.SharePointContainers.WebApplication.Url;

            using (SPSite site = new SPSite(TestContent.SharePointContainers.SiCInActive.Url))
            {
                SPWebApplication parent = site.WebApplication;

                // Act

                var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthyWebApp.Id];
                // web app features cannot easily be reproduced in faulty state
                // var testFeatureFaulty = parent.Features[TestContent.TestFeatures.FaultyWebApp.Id];

                var featureParent = FeatureParent.GetFeatureParent(parent);


                var sharePointFeatureHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy);
                var sharePointFeatureRetrievedWithParentHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy, featureParent);

                // Assert
                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Id, sharePointFeatureHealthy.Id);
                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Name, sharePointFeatureHealthy.Name);
                Assert.Equal(featureParent.Url, sharePointFeatureHealthy.Parent.Url);
                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Version, sharePointFeatureHealthy.Version);
                Assert.Equal(parentScope, sharePointFeatureHealthy.Scope);
                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Faulty, sharePointFeatureHealthy.Faulty);

                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Id, sharePointFeatureRetrievedWithParentHealthy.Id);
                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Name, sharePointFeatureRetrievedWithParentHealthy.Name);
                Assert.Equal(featureParent.Url, sharePointFeatureRetrievedWithParentHealthy.Parent.Url);
                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Version, sharePointFeatureRetrievedWithParentHealthy.Version);
                Assert.Equal(parentScope, sharePointFeatureRetrievedWithParentHealthy.Scope);
                Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Faulty, sharePointFeatureRetrievedWithParentHealthy.Faulty);
            }
        }

        [Fact]
        public void CanGetActivatedFeatureFromFarm()
        {
            // Arrange
            var parentScope = SPFeatureScope.Farm;
            // var parentUrl = TestContent.SharePointContainers.WebApplication.Url;


            SPWebService farm = SPWebService.ContentService;

            var parent = farm;

            // Act

            var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthyFarm.Id];
            // web app features cannot easily be reproduced in faulty state
            // var testFeatureFaulty = parent.Features[TestContent.TestFeatures.FaultyFarm.Id];

            var featureParent = FeatureParent.GetFeatureParent(parent);


            var sharePointFeatureHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy);
            var sharePointFeatureRetrievedWithParentHealthy = ActivatedFeature.GetActivatedFeature(testFeatureHealthy, featureParent);

            // Assert
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Id, sharePointFeatureHealthy.Id);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Name, sharePointFeatureHealthy.Name);
            Assert.Equal(featureParent.Url, sharePointFeatureHealthy.Parent.Url);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Version, sharePointFeatureHealthy.Version);
            Assert.Equal(parentScope, sharePointFeatureHealthy.Scope);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Faulty, sharePointFeatureHealthy.Faulty);

            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Id, sharePointFeatureRetrievedWithParentHealthy.Id);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Name, sharePointFeatureRetrievedWithParentHealthy.Name);
            Assert.Equal(featureParent.Url, sharePointFeatureRetrievedWithParentHealthy.Parent.Url);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Version, sharePointFeatureRetrievedWithParentHealthy.Version);
            Assert.Equal(parentScope, sharePointFeatureRetrievedWithParentHealthy.Scope);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Faulty, sharePointFeatureRetrievedWithParentHealthy.Faulty);
        }
    }
}

