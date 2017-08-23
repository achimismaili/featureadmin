using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Serilog;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.SharePoint2013.Factories
{
    public class FeatureParent : Interfaces.IFeatureParent
    {
        public string DisplayName { get; private set; }
        public Guid Id { get; private set; }

        public int ChildCount { get; set; }

        public string Url { get; private set; }

        public SPFeatureScope Scope { get; private set; }

        public List<FeatureParent> ChildLocations { get; private set; }

        public List<ActivatedFeature> ActivatedFeatures { get; private set; }

        /// <summary>
        /// only available for Scope SiteCollection
        /// </summary>
        public Guid? ContentDatabaseId { get; private set; }

        /// <summary>
        /// abbreviated scope to max 3 letters
        /// </summary>
        public string ScopeMatchCode { get {
                switch (Scope)
                {
                    case SPFeatureScope.Farm:
                        return "FRM";
                    case SPFeatureScope.WebApplication:
                        return "WA";
                    case SPFeatureScope.Site:
                        return "SC";
                    case SPFeatureScope.Web:
                        return "WEB";
                    case SPFeatureScope.ScopeInvalid:
                    default:
                        return "ERR";
                }
            } }

        /// <summary>
        /// Only for Factory Methods of this class
        /// </summary>
        private FeatureParent()
        {
            ChildLocations = new List<FeatureParent>();
            // ActivatedFeatures = new List<ActivatedFeature>();
        }

        public FeatureParent(string displayName, string url, Guid id, SPFeatureScope scope, Guid? contentDatabaseId = null)
        {
            DisplayName = displayName;
            Url = url;
            Id = id;
            Scope = scope;
            ContentDatabaseId = contentDatabaseId;
        }

        public static FeatureParent GetFeatureParent(SPWebService farmWebService)
        {
            FeatureParent p = null;
            try
            {
                if (farmWebService == null)
                {
                    return GetFeatureParentUndefined();
                }
                p = new FeatureParent()
                {
                    DisplayName = "Farm",
                    Url = "Farm",
                    Id = farmWebService.Id,
                    Scope = SPFeatureScope.Farm
                };
            }
            catch (Exception ex)
            {
                Log.Error("Error when trying to get Farm object.", ex);
                return GetFeatureParentUndefined(ex.Message);
            }

            try
            {
                var farmFeatures = farmWebService.Features;
                p.ActivatedFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(farmFeatures, p);
            }
            catch (Exception ex)
            {
                Log.Error("Error when trying to load Farm features.", ex);
            }
            return p;
        }

        public static FeatureParent GetFeatureParent(SPSite siCo)
        {
            FeatureParent p = null;
            string locationUrl = string.Empty;
            try
            {
                if (siCo == null)
                {
                    return GetFeatureParentUndefined();
                }

                locationUrl = siCo.Url;

                p = new FeatureParent()
                {
                    DisplayName = siCo.RootWeb.Title,
                    Url = locationUrl,
                    Id = siCo.ID,
                    Scope = SPFeatureScope.Site
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    string.Format(
                        "Error when trying to get SiteCollection {0}.",
                        locationUrl
                        ),
                    ex);
                return GetFeatureParentUndefined(ex.Message);
            }

            try
            {
                var features = siCo.Features;
                p.ActivatedFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(features, p);
            }
            catch (Exception ex)
            {
                Log.Error(
                    string.Format(
                        "Error when trying to load features from {0}.",
                        locationUrl
                        ),
                    ex);
            }
            return p;
        }

        public static FeatureParent GetFeatureParent(SPWeb web)
        {
            FeatureParent p = null;
            string locationUrl = string.Empty;
            try
            {
                if (web == null)
                {
                    return GetFeatureParentUndefined();
                }

                locationUrl = web.Url;

                p = new FeatureParent()
                {
                    DisplayName = web.Title, // + " (" + web.Name + ")",
                    Url = locationUrl,
                    Id = web.ID,
                    Scope = SPFeatureScope.Web
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                   string.Format(
                       "Error when trying to get web {0}.",
                       locationUrl
                       ),
                   ex);
                return GetFeatureParentUndefined(ex.Message);
            }

            try
            {
                var features = web.Features;
                p.ActivatedFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(features, p);
            }
            catch (Exception ex)
            {
                Log.Error(
                    string.Format(
                        "Error when trying to load features from web {0}.",
                        locationUrl
                        ),
                    ex);
            }
            return p;

        }

        public static FeatureParent GetFeatureParent(SPWebApplication webApp, string name = "")
        {
            FeatureParent p = null;
            string locationUrl = string.Empty;
            try
            {
                if (webApp == null)
                {
                    return GetFeatureParentUndefined();
                }

                locationUrl = webApp.GetResponseUri(SPUrlZone.Default).ToString();

                p = new FeatureParent()
                {
                    DisplayName = string.IsNullOrEmpty(name) ? webApp.Name : name , // + " (" + web.Name + ")",
                    Url = locationUrl,
                    Id = webApp.Id,
                    Scope = SPFeatureScope.WebApplication
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                   string.Format(
                       "Error when trying to get web app {0}.",
                       locationUrl
                       ),
                   ex);
                return GetFeatureParentUndefined(ex.Message);
            }

            try
            {
                var features = webApp.Features;
                p.ActivatedFeatures = ActivatedFeature.MapSpFeatureToActivatedFeature(features, p);
            }
            catch (Exception ex)
            {
                Log.Error(
                    string.Format(
                        "Error when trying to load features from web app {0}.",
                        locationUrl
                        ),
                    ex);
            }
            return p;
        }

        public static FeatureParent GetFeatureParentUndefined(string displayName = "")
        {
            var p = new FeatureParent()
            {
                DisplayName = string.IsNullOrEmpty(displayName) ? "undefined" : displayName,
                Url = "undefined",
                Id = Guid.Empty,
                Scope = SPFeatureScope.ScopeInvalid,
                ActivatedFeatures = new List<ActivatedFeature>()
            };
            return p;
        }

        public static FeatureParent GetFeatureParent(SPFeature feature, SPFeatureScope scope)
        {
            try
            {
                if (feature != null && feature.Parent != null && scope != SPFeatureScope.ScopeInvalid)
                {
                    switch (scope)
                    {
                        case SPFeatureScope.Farm:
                            var f = feature.Parent as SPWebService; // SPWebService.ContentService.Features
                            return GetFeatureParent(f);
                        case SPFeatureScope.WebApplication:
                            var wa = feature.Parent as SPWebApplication;
                            return GetFeatureParent(wa);
                        case SPFeatureScope.Site:
                            var s = feature.Parent as SPSite;
                            return GetFeatureParent(s);
                        case SPFeatureScope.Web:
                            var w = feature.Parent as SPWeb;
                            return GetFeatureParent(w);
                        default:
                            return GetFeatureParentUndefined();
                    }
                }
                else
                {
                    return GetFeatureParentUndefined();
                }
            }
            catch (Exception ex)
            {
                return GetFeatureParentUndefined(ex.Message);
            }
        }

        /// <summary>
        /// Gets a FEatureParent instance from a standard SharePoint SPFeature object
        /// </summary>
        /// <param name="feature">SharePoint SPFeature</param>
        /// <returns>Instance of FeatureParent for the feature parent object</returns>
        /// <remarks>challenge here is to identify, which type the parent is</remarks>
        public static FeatureParent GetFeatureParent(SPFeature feature)
        {
            if (feature == null | feature.Parent == null)
            {
                return GetFeatureParentUndefined();
            }

            // check type of parent

            if (feature.Parent is SPWeb)
            {
                return GetFeatureParent(feature.Parent as SPWeb);
            }

            if (feature.Parent is SPSite )
            {
                return GetFeatureParent(feature.Parent as SPSite);
            }

            if (feature.Parent is SPWebApplication)
            {
                return GetFeatureParent(feature.Parent as SPWebApplication);
            }

            if (feature.Parent is SPWebService )
            {
                return GetFeatureParent(feature.Parent as SPWebService);
            }

            return GetFeatureParentUndefined();
        }
    }
}