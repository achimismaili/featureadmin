using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Models
{
    public class FeatureParent : Interfaces.IFeatureParent
    {
        public string DisplayName { get; private set; }
        public Guid Id { get; private set; }

        public string Url { get; private set; }

        public SPFeatureScope Scope { get; private set; }

        /// <summary>
        /// only available for Scope SiteCollection
        /// </summary>
        public Guid? ContentDatabaseId { get; private set; }

        /// <summary>
        /// Only for Factory Methods of this class
        /// </summary>
        private FeatureParent()
        {
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
            try
            {
                if (farmWebService == null)
                {
                    return GetFeatureParentUndefined();
                }
                var p = new FeatureParent()
                {
                    DisplayName = "Farm",
                    Url = "Farm",
                    Id = farmWebService.Id,
                    Scope = SPFeatureScope.Farm
                };
                return p;
            }
            catch (Exception ex)
            {
                return GetFeatureParentUndefined(ex.Message);
            }
        }

        public static FeatureParent GetFeatureParent(SPSite siCo)
        {
            try
            {
                if (siCo == null)
                {
                    return GetFeatureParentUndefined();
                }

                var p = new FeatureParent()
                {
                    DisplayName = siCo.RootWeb.Title,
                    Url = siCo.Url,
                    Id = siCo.ID,
                    Scope = SPFeatureScope.Site
                };
                return p;
            }
            catch (Exception ex)
            {
                return GetFeatureParentUndefined(ex.Message);
            }
        }

        public static FeatureParent GetFeatureParent(SPWeb web)
        {
            try
            {
                if (web == null)
                {
                    return GetFeatureParentUndefined();
                }

                var p = new FeatureParent()
                {
                    DisplayName = web.Title, // + " (" + web.Name + ")",
                    Url = web.Url,
                    Id = web.ID,
                    Scope = SPFeatureScope.Web
                };
                return p;
            }
            catch (Exception ex)
            {
                return GetFeatureParentUndefined(ex.Message);
            }
        }

        public static FeatureParent GetFeatureParent(SPWebApplication webApp, string name = "")
        {
            try
            {
                if (webApp == null)
                {
                    return GetFeatureParentUndefined();
                }

                var p = new FeatureParent()
                {
                    DisplayName = string.IsNullOrEmpty(name) ? webApp.Name : name , // + " (" + web.Name + ")",
                    Url = webApp.GetResponseUri(SPUrlZone.Default).ToString(),
                    Id = webApp.Id,
                    Scope = SPFeatureScope.WebApplication
                };
                return p;
            }
            catch (Exception ex)
            {
                return GetFeatureParentUndefined(ex.Message);
            }
        }

        public static FeatureParent GetFeatureParentUndefined(string displayName = "")
        {
            var p = new FeatureParent()
            {
                DisplayName = string.IsNullOrEmpty(displayName) ? "undefined" : displayName,
                Url = "undefined",
                Id = Guid.Empty,
                Scope = SPFeatureScope.ScopeInvalid
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