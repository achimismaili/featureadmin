using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService
{
    public static class GetActivatedFeatures
    {
        public static List<ActivatedFeature> GetAllFromFarm()
        {
            var allActivatedFeatures = new List<ActivatedFeature>();

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                // Get Farm features
                try
                {
                    var farmFeatures = SPWebService.ContentService.Features;

                    if (farmFeatures != null)
                    {
                        var activatedFarmFeatures = ActivatedFeatureFactory.MapSpFeatureToActivatedFeature(farmFeatures, SPWebService.ContentService);
                        allActivatedFeatures.AddRange(activatedFarmFeatures);
                    }
                }
                catch (Exception)
                {
                    //TODO log an error in log window
                }

                // Get Web App features

                // Central Admin
                var caIndex = 1;
                foreach (SPWebApplication adminApp in SPWebService.AdministrationService.WebApplications)
                {
                    if (adminApp != null)
                    {
                        var adminFeatures = adminApp.Features;

                        if (adminFeatures != null && adminFeatures.Count > 0)
                        {
                            var index = (adminApp.Features.Count == 1 && caIndex == 1) ? string.Empty : " " + caIndex.ToString();

                            var caParent = FeatureParent.GetFeatureParent(adminApp, "Central Admin" + index);

                            var activatedCaWebAppFeatures = ActivatedFeatureFactory.MapSpFeatureToActivatedFeature(adminFeatures, caParent);
                            allActivatedFeatures.AddRange(activatedCaWebAppFeatures);
                        }

                        var sites = adminApp.Sites;

                        if (sites != null && sites.Count > 0)
                        {
                            foreach (SPSite s in sites)
                            {

                                var activatedSiCoFeatures = GetSiteFeatuesAndBelow(s);
                                allActivatedFeatures.AddRange(activatedSiCoFeatures);

                                s.Dispose();
                            }
                        }
                    }
                }

                // Content Web Apps
                foreach (SPWebApplication webApp in SPWebService.ContentService.WebApplications)
                {
                    if (webApp != null)
                    {
                        var waFeatures = webApp.Features;

                        if (waFeatures != null && waFeatures.Count > 0)
                        {
                            var activatedWebAppFeatures = ActivatedFeatureFactory.MapSpFeatureToActivatedFeature(waFeatures, webApp);
                            allActivatedFeatures.AddRange(activatedWebAppFeatures);
                        }

                        var sites = webApp.Sites;

                        if (sites != null && sites.Count > 0)
                        {
                            foreach (SPSite s in sites)
                            {
                                var activatedSiCoFeatures = GetSiteFeatuesAndBelow(s);
                                allActivatedFeatures.AddRange(activatedSiCoFeatures);

                                s.Dispose();
                            }
                        }
                    }
                }
            });
                return allActivatedFeatures;
        }

        private static IEnumerable<ActivatedFeature> GetSiteFeatuesAndBelow(SPSite site)
        {
            var allActivatedFeatures = new List<ActivatedFeature>();

            if (site != null)
            {
                var siteFeatures = site.Features;

                if (siteFeatures != null && siteFeatures.Count > 0)
                {
                    var activatedSiCoFeatures = ActivatedFeatureFactory.MapSpFeatureToActivatedFeature(siteFeatures, site);
                    allActivatedFeatures.AddRange(activatedSiCoFeatures);
                }

                var webs = site.AllWebs;

                if (webs != null && webs.Count > 0)
                {
                    foreach (SPWeb w in webs)
                    {
                        var activatedWebFeatures = GetWebFeatures(w);
                        allActivatedFeatures.AddRange(activatedWebFeatures);

                        w.Dispose();
                    }
                }
            }

            return allActivatedFeatures;
        }

        private static IEnumerable<ActivatedFeature> GetWebFeatures(SPWeb web)
        {
            var allActivatedFeatures = new List<ActivatedFeature>();

            if (web != null)
            {
                var webFeatures = web.Features;

                if (webFeatures != null && webFeatures.Count > 0)
                {
                    var activatedWebFeatures = ActivatedFeatureFactory.MapSpFeatureToActivatedFeature(webFeatures, web);
                    allActivatedFeatures.AddRange(activatedWebFeatures);
                }
            }

            return allActivatedFeatures;
        }
    }
}