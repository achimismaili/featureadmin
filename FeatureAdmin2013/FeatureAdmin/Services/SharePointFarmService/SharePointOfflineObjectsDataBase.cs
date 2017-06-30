using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Services.SharePointFarmService
{
    /// <summary>
    /// sp in membory database as singleton
    /// </summary>
    /// <remarks>see also
    /// https://msdn.microsoft.com/en-us/library/ff650316.aspx
    /// </remarks>
    public class SharePointOfflineObjectsDataBase
    {
        public List<ActivatedFeature> ActivatedFeatures { get; private set; }
        public List<FeatureDefinition> FeatureDefinitions { get; private set; }
        public Dictionary<Guid, List<FeatureParent>> SharePointParentHierarchy { get; private set; }
        public Guid FarmId { get; private set; }

        private static SharePointOfflineObjectsDataBase singletonInstance;
        /// <summary>
        /// SharePointDataBase as singleton
        /// </summary>
        /// <remarks>
        /// see also https://msdn.microsoft.com/en-us/library/ff650316.aspx
        /// </remarks>
        public static SharePointOfflineObjectsDataBase SingletonInstance
        {
            get
            {
                if (singletonInstance == null)
                {
                    singletonInstance = new SharePointOfflineObjectsDataBase();
                }
                return singletonInstance;
            }
        }

        /// <summary>
        /// private ctor, db can only be generated via static property
        /// </summary>
        private SharePointOfflineObjectsDataBase()
        {
            // Get feature definitions
            FeatureDefinitions = LoadAllFeatureDefinitions();

            // Get activated features and build up SharePointParentHierarchy
            LoadAllActivatedFeaturesAndHierarchy();

            // Add instances of activated features to each feature definition
            MapActivatedFeaturesToDefinitions();
        }

        /// <summary>
        /// Get activated features from farm and build up SharePointParentHierarchy
        /// </summary>
        private void LoadAllActivatedFeaturesAndHierarchy()
        {
            ActivatedFeatures = new List<ActivatedFeature>();

            SharePointParentHierarchy = new Dictionary<Guid, List<FeatureParent>>();

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                var farm = ReadViaSharePointApi.GetFarm();
                // Get Farm features
                var farmFeatures = farm.Features;

                var parent = FeatureParent.GetFeatureParent(farm);

                FarmId = parent.Id;

                SharePointParentHierarchy.Add(FarmId, new List<FeatureParent>());

                if (farmFeatures != null)
                {
                    var activatedFarmFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(farmFeatures, parent);

                    ActivatedFeatures.AddRange(activatedFarmFeatures);
                }

                // Get Web App features

                var adminWebApps = ReadViaSharePointApi.GetWebApplicationsAdmin();

                // Central Admin
                var caIndex = 1;
                foreach (SPWebApplication adminApp in adminWebApps)
                {
                    if (adminApp != null)
                    {
                        var index = (adminApp.Features.Count == 1 && caIndex == 1) ? string.Empty : " " + caIndex.ToString();
                        var caParent = FeatureParent.GetFeatureParent(adminApp, "Central Admin" + index);

                        AddParentToHierarchy(FarmId, caParent, true);

                        var adminFeatures = adminApp.Features;

                        if (adminFeatures != null && adminFeatures.Count > 0)
                        {
                            var activatedCaWebAppFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(adminFeatures, caParent);
                            ActivatedFeatures.AddRange(activatedCaWebAppFeatures);
                        }

                        var sites = adminApp.Sites;

                        if (sites != null && sites.Count > 0)
                        {
                            foreach (SPSite s in sites)
                            {
                                GetSiteFeatuesAndBelow(s, caParent.Id);
                                s.Dispose();
                            }
                        }
                    }
                }

                // Content Web Apps
                var contentWebApps = ReadViaSharePointApi.GetWebApplicationsContent();

                foreach (SPWebApplication webApp in contentWebApps)
                {
                    if (webApp != null)
                    {
                        var waAsParent = FeatureParent.GetFeatureParent(webApp);

                        AddParentToHierarchy(FarmId, waAsParent, true);

                        var waFeatures = webApp.Features;

                        if (waFeatures != null && waFeatures.Count > 0)
                        {
                            var activatedWebAppFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(waFeatures, waAsParent);
                            ActivatedFeatures.AddRange(activatedWebAppFeatures);
                        }

                        var sites = webApp.Sites;

                        if (sites != null && sites.Count > 0)
                        {
                            foreach (SPSite s in sites)
                            {
                                GetSiteFeatuesAndBelow(s, waAsParent.Id);
                                s.Dispose();
                            }
                        }
                    }
                }
            });
        }

        private void GetSiteFeatuesAndBelow(SPSite site, Guid webAppId)
        {
            if (site != null)
            {
                var parent = FeatureParent.GetFeatureParent(site);

                AddParentToHierarchy(webAppId, parent, true);

                var siteFeatures = site.Features;

                if (siteFeatures != null && siteFeatures.Count > 0)
                {
                    var activatedSiCoFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(siteFeatures, parent);
                    ActivatedFeatures.AddRange(activatedSiCoFeatures);
                }

                var webs = site.AllWebs;

                if (webs != null && webs.Count > 0)
                {
                    foreach (SPWeb w in webs)
                    {
                        GetWebFeatures(w, site.ID);
                        w.Dispose();
                    }
                }
            }
        }

        private void GetWebFeatures(SPWeb web, Guid SiteCollectionId)
        {
            if (web != null)
            {
                var parent = FeatureParent.GetFeatureParent(web);

                AddParentToHierarchy(SiteCollectionId, parent, false);

                var webFeatures = web.Features;

                if (webFeatures != null && webFeatures.Count > 0)
                {
                    var activatedWebFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(webFeatures, parent);
                    ActivatedFeatures.AddRange(activatedWebFeatures);
                }
            }
        }

        /// <summary>
        /// final step in setting up database
        /// </summary>
        private void MapActivatedFeaturesToDefinitions()
        {
            if (ActivatedFeatures != null && ActivatedFeatures.Any())
            {
                var distinctActivatedFeatureIds = ActivatedFeatures.Select(af => af.Id).Distinct();

                foreach (Guid featureId in distinctActivatedFeatureIds)
                {
                    var activatedFeatureGroup = ActivatedFeatures.Where(af => af.Id == featureId);

                    var featureDef = FeatureDefinitions.FirstOrDefault(fd => fd.Id == featureId);

                    if (featureDef != null)
                    {
                        // add activated features to feature definition
                        featureDef.ActivatedFeatures.AddRange(activatedFeatureGroup);
                    }
                    else
                    {
                        // fyi - if we get here, we have most likely a group of faulty features ...

                        // create feature definition and add features
                        var newFeatureDef = FeatureDefinition.GetFeatureDefinition(activatedFeatureGroup);
                        FeatureDefinitions.Add(newFeatureDef);
                    }
                }
            }
        }

        /// <summary>
        /// To set up a SharePoint hierarchy of web apps, site collections and webs, this method sets up the hierarchy tree
        /// </summary>
        /// <param name="parentOfThisParent">the parent container of this parent in the sharepoint hierarchy</param>
        /// <param name="parent">SharePoint container to add to hierarchy</param>
        /// <param name="hasChildren">if this container itself has children, new entry is created in hierarchy collection</param>
        private void AddParentToHierarchy(Guid parentOfThisParent, FeatureParent parent, bool hasChildren)
        {
            if (parent == null)
            {
                return;
            }

            //if (SharePointParentHierarchy.ContainsKey(parentOfThisParent))
            //{
             SharePointParentHierarchy[parentOfThisParent].Add(parent);
            //}


            if (hasChildren)
            {
                SharePointParentHierarchy.Add(parent.Id, new List<FeatureParent>());
            }
        }

        private List<FeatureDefinition> LoadAllFeatureDefinitions()
        {
            var spDefs = ReadViaSharePointApi.GetFeatureDefinitionCollection();

            return FeatureDefinition.GetFeatureDefinition(spDefs);
        }

    }
}
