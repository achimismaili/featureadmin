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
    public class FeatureParentTest
    {
        [Fact]
        public void CanGetFeatureParentFromWeb()
        {
            // Arrange
            var parentScope = SPFeatureScope.Web;
            var parentUrl = TestContent.SharePointContainers.SiCoActivated.Url;

            using (SPSite site = new SPSite(parentUrl))
            {
                using (SPWeb parent = site.OpenWeb())
                {

                    var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthyWeb.Id];
                    var testFeatureFaulty = parent.Features[TestContent.TestFeatures.FaultyWeb.Id];

                    // Act
                    var parentHealthy = FeatureParent.GetFeatureParent(testFeatureHealthy);
                    var parentFaulty = FeatureParent.GetFeatureParent(testFeatureFaulty);
                    var parentRetrievedWithScopeHealthy = FeatureParent.GetFeatureParent(testFeatureHealthy, parentScope);
                    var parentRetrievedWithScopeFaulty = FeatureParent.GetFeatureParent(testFeatureFaulty, parentScope);

                    // Assert
                    Assert.Equal(parent.Title, parentHealthy.DisplayName);
                    Assert.Equal(parent.ID, parentHealthy.Id);
                    Assert.Equal(parentUrl, parentHealthy.Url);
                    Assert.Equal(parentScope, parentHealthy.Scope);

                    Assert.Equal(parent.Title, parentFaulty.DisplayName);
                    Assert.Equal(parent.ID, parentFaulty.Id);
                    Assert.Equal(parentUrl, parentFaulty.Url);
                    Assert.Equal(parentScope, parentFaulty.Scope);

                    Assert.Equal(parent.Title, parentRetrievedWithScopeHealthy.DisplayName);
                    Assert.Equal(parent.ID, parentRetrievedWithScopeHealthy.Id);
                    Assert.Equal(parentUrl, parentRetrievedWithScopeHealthy.Url);
                    Assert.Equal(parentScope, parentRetrievedWithScopeHealthy.Scope);

                    Assert.Equal(parent.Title, parentRetrievedWithScopeFaulty.DisplayName);
                    Assert.Equal(parent.ID, parentRetrievedWithScopeFaulty.Id);
                    Assert.Equal(parentUrl, parentRetrievedWithScopeFaulty.Url);
                    Assert.Equal(parentScope, parentRetrievedWithScopeFaulty.Scope);
                }
            }
        }

        [Fact]
        public void CanGetFeatureParentFromSite()
        {
            // Arrange
            var parentScope = SPFeatureScope.Site;
            var parentUrl = TestContent.SharePointContainers.SiCoActivated.Url;

            using (SPSite parent = new SPSite(parentUrl))
            {
                var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthySite.Id];
                var testFeatureFaulty = parent.Features[TestContent.TestFeatures.HealthySite.Id];

                // Act
                var parentHealthy = FeatureParent.GetFeatureParent(testFeatureHealthy);
                var parentFaulty = FeatureParent.GetFeatureParent(testFeatureFaulty);
                var parentRetrievedWithScopeHealthy = FeatureParent.GetFeatureParent(testFeatureHealthy, parentScope);
                var parentRetrievedWithScopeFaulty = FeatureParent.GetFeatureParent(testFeatureFaulty, parentScope);

                // Assert
                Assert.Equal(parent.RootWeb.Title, parentHealthy.DisplayName);
                Assert.Equal(parent.ID, parentHealthy.Id);
                Assert.Equal(parentUrl, parentHealthy.Url);
                Assert.Equal(parentScope, parentHealthy.Scope);

                Assert.Equal(parent.RootWeb.Title, parentFaulty.DisplayName);
                Assert.Equal(parent.ID, parentFaulty.Id);
                Assert.Equal(parentUrl, parentFaulty.Url);
                Assert.Equal(parentScope, parentFaulty.Scope);

                Assert.Equal(parent.RootWeb.Title, parentRetrievedWithScopeHealthy.DisplayName);
                Assert.Equal(parent.ID, parentRetrievedWithScopeHealthy.Id);
                Assert.Equal(parentUrl, parentRetrievedWithScopeHealthy.Url);
                Assert.Equal(parentScope, parentRetrievedWithScopeHealthy.Scope);

                Assert.Equal(parent.RootWeb.Title, parentRetrievedWithScopeFaulty.DisplayName);
                Assert.Equal(parent.ID, parentRetrievedWithScopeFaulty.Id);
                Assert.Equal(parentUrl, parentRetrievedWithScopeFaulty.Url);
                Assert.Equal(parentScope, parentRetrievedWithScopeFaulty.Scope);




            }
        }

        [Fact]
        public void CanGetFeatureParentFromWebApp()
        {
            // Arrange
            var parentScope = SPFeatureScope.WebApplication;
            var parentUrl = TestContent.SharePointContainers.WebApplication.Url;

            using (SPSite site = new SPSite(TestContent.SharePointContainers.SiCInActive.Url))
            {
                SPWebApplication parent = site.WebApplication;


            var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthyWebApp.Id];
               // var testFeatureFaulty = parent.Features[TestContent.TestFeatures.FaultyWebApp.Id];

                // Act
                var parentRetrievedWithScope = FeatureParent.GetFeatureParent(testFeatureHealthy, parentScope);
                var parentRetrievedNoScope = FeatureParent.GetFeatureParent(testFeatureHealthy);


                // Assert
                Assert.Equal(parent.Name, parentRetrievedWithScope.DisplayName);
                Assert.Equal(parent.Id, parentRetrievedWithScope.Id);
                Assert.Equal(parentUrl, parentRetrievedWithScope.Url);
                Assert.Equal(parentScope, parentRetrievedWithScope.Scope);

                Assert.Equal(parent.Name, parentRetrievedNoScope.DisplayName);
                Assert.Equal(parent.Id, parentRetrievedNoScope.Id);
                Assert.Equal(parentUrl, parentRetrievedNoScope.Url);
                Assert.Equal(parentScope, parentRetrievedNoScope.Scope);
            }
        }

        [Fact]
        public void CanGetFetaureParentFromFarm()
        {
            // Arrange
            var parentScope = SPFeatureScope.Farm;
            // var parentUrl = TestContent.SharePointContainers.WebApplication.Url;

            
                SPWebService parent = SPWebService.ContentService;


                var testFeatureHealthy = parent.Features[TestContent.TestFeatures.HealthyFarm.Id];
                // var testFeatureFaulty = parent.Features[TestContent.TestFeatures.FaultyFarm.Id];

                // Act
                var parentRetrievedWithScope = FeatureParent.GetFeatureParent(testFeatureHealthy, parentScope);
                var parentRetrievedNoScope = FeatureParent.GetFeatureParent(testFeatureHealthy);


                // Assert
                Assert.Equal("Farm", parentRetrievedWithScope.DisplayName);
                Assert.Equal(parent.Id, parentRetrievedWithScope.Id);
                Assert.Equal("Farm", parentRetrievedWithScope.Url);
                Assert.Equal(parentScope, parentRetrievedWithScope.Scope);

                Assert.Equal("Farm", parentRetrievedNoScope.DisplayName);
                Assert.Equal(parent.Id, parentRetrievedNoScope.Id);
                Assert.Equal("Farm", parentRetrievedNoScope.Url);
                Assert.Equal(parentScope, parentRetrievedNoScope.Scope );
            }
        }
}
