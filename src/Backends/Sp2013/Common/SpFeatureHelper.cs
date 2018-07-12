using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Sp2013.Common
{
    public static class SpFeatureHelper
    {
        /// <summary>
        /// activate a feature
        /// </summary>
        /// <param name="features"></param>
        /// <param name="featureId"></param>
        /// <param name="force"></param>
        /// <returns>the activated feature</returns>
        /// <remarks>attention, might throw exception!</remarks>
        internal static SPFeature ActivateFeatureInFeatureCollection(SPFeatureCollection features, Guid featureId, bool force)
        {
            SPFeature spFeature = features.Add(featureId, force);

            if (spFeature == null)
            {
                var errMsg = string.Format("Feature activation for feature '{0}' failed.", featureId);

                if (!force)
                {
                    errMsg += " You might want to try again with 'force' enabled.";
                }

                throw new ApplicationException(errMsg);
            }

            return spFeature;
        }

        /// <summary>
        /// deactivate a feature
        /// </summary>
        /// <param name="features"></param>
        /// <param name="featureId"></param>
        /// <param name="force"></param>
        /// <remarks>attention, might throw exception!</remarks>
        internal static void DeactivateFeatureInFeatureCollection(SPFeatureCollection features, Guid featureId, bool force)
        {
                var featuresActiveBefore = features.Count();
                features.Remove(featureId, force);
           
            if (!(featuresActiveBefore > features.Count))
            {
                var errMsg = string.Format("Feature '{0}' was not removed.", featureId);

                if (!force)
                {
                    errMsg += " You might want to try again with 'force' enabled.";
                }

                throw new ApplicationException(errMsg);
            }

            return;
        }


        private static SPFeatureCollection GetFeatureCollection (SPWeb web)
        {
            if (web == null)
            {
                return null;
            }

            return web.Features;
        }

        private static SPFeatureCollection GetFeatureCollection(SPSite site)
        {
            if (site == null)
            {
                return null;
            }

            return site.Features;
        }

        internal static SPFeatureCollection GetFeatureCollection(SPWeb spWeb, bool elevatedPrivileges)
        {
            SPFeatureCollection featureCollection;

            if (elevatedPrivileges)
            {
                featureCollection = SpSiteElevation.SelectAsSystem(spWeb, GetFeatureCollection);
            }
            else
            {
                featureCollection = spWeb.Features;
            }

            return featureCollection;
        }

        internal static SPFeatureCollection GetFeatureCollection(SPSite spSite, bool elevatedPrivileges)
        {
            SPFeatureCollection featureCollection;

            if (elevatedPrivileges)
            {
                featureCollection = SpSiteElevation.SelectAsSystem(spSite, GetFeatureCollection);
            }
            else
            {
                featureCollection = spSite.Features;
            }

            return featureCollection;
        }

    }
}
