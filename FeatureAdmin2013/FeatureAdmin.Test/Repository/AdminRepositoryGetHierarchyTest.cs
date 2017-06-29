using FeatureAdmin.Repository;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeatureAdmin.Test.Repository
{
    public class AdminRepositoryGetHierarchyTest 
    {
        private AdminRepository repository;

        private Guid webAppId;
        private Guid siCoActivatedId;
        private Guid siCoActivatedRootWebId;
        private Guid siCoActivatedSubWebActivatedId;
        private Guid siCoActivatedSubWebInactiveId;
        private Guid siCoInactiveId;
        private Guid siCoInactiveRootWebId;
        private Guid siCoInactiveSubWebActivatedId;
        private Guid siCoInactiveSubWebInactiveId;

        public AdminRepositoryGetHierarchyTest()
        {
            // Arrange
            repository = new AdminRepository();

            // SiCo Activated
            using (SPSite site = new SPSite(TestContent.SharePointContainers.SiCoActivated.Url))
            {
                siCoActivatedId = site.ID;

                webAppId = site.WebApplication.Id;

                // RootWeb
                using (SPWeb web = site.OpenWeb())
                {
                    siCoActivatedRootWebId = web.ID;
                }
                // SiCoActivatedSubWebActivated
                using (SPWeb web = site.OpenWeb(TestContent.SharePointContainers.SiCoActivated.SubWebActivated.UrlRelative))
                {
                    siCoActivatedSubWebActivatedId = web.ID;
                }

                // SiCoActivatedSubWebInactive
                using (SPWeb web = site.OpenWeb(TestContent.SharePointContainers.SiCoActivated.SubWebInactive.UrlRelative))
                {
                    siCoActivatedSubWebInactiveId = web.ID;
                }
            }

            // SiCo Inactive
            using (SPSite site = new SPSite(TestContent.SharePointContainers.SiCoInActive.Url))
            {
                siCoInactiveId = site.ID;
                // RootWeb
                using (SPWeb web = site.OpenWeb())
                {
                    siCoInactiveRootWebId = web.ID;
                }
                // SiCoInactiveSubWebActivated
                using (SPWeb web = site.OpenWeb(TestContent.SharePointContainers.SiCoInActive.SubWebActivated.UrlRelative))
                {
                    siCoInactiveSubWebActivatedId = web.ID;
                }

                // SiCoInactiveSubWebInactive
                using (SPWeb web = site.OpenWeb(TestContent.SharePointContainers.SiCoInActive.SubWebInactive.UrlRelative))
                {
                    siCoInactiveSubWebInactiveId = web.ID;
                }
            }
        }

        [Fact]
        public void ReceiveWebAppsContainsTestWebApp()
        {
            //Act
            var webApps = repository.GetSharePointWebApplications();
            var webApp = webApps.FirstOrDefault(w => w.Id == webAppId);

            //Assert
            Assert.NotNull(webApp);
            Assert.Equal(TestContent.SharePointContainers.WebApplication.Url, webApp.Url);
        }

        [Fact]
        public void TestWebAppChildrenContainSiCos()
        {
            //Act
            var sites = repository.GetSharePointChildHierarchy(webAppId);

            var siteActivated = sites.FirstOrDefault(s => s.Id == siCoActivatedId);
            var siteInactive = sites.FirstOrDefault(s => s.Id == siCoInactiveId);

            //Assert
            Assert.NotNull(siteActivated);
            Assert.Equal(TestContent.SharePointContainers.SiCoActivated.Url, siteActivated.Url);
            Assert.NotNull(siteInactive);
            Assert.Equal(TestContent.SharePointContainers.SiCoInActive.Url, siteInactive.Url);
        }

        [Fact]
        public void TestSiCoChildrenContainWebs()
        {
            //Act
            var webs = repository.GetSharePointChildHierarchy(siCoActivatedId);

            var wActivated = webs.FirstOrDefault(s => s.Id == siCoActivatedSubWebActivatedId);
            var wInactive = webs.FirstOrDefault(s => s.Id == siCoActivatedSubWebInactiveId);

            //Assert
            Assert.Equal(3, webs.Count);
            Assert.NotNull(wActivated);
            Assert.Equal(TestContent.SharePointContainers.SiCoActivated.SubWebActivated.Url, wActivated.Url);
            Assert.NotNull(wInactive);
            Assert.Equal(TestContent.SharePointContainers.SiCoActivated.SubWebInactive.Url, wInactive.Url);
        }
    }
}
