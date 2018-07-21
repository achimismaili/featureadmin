using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Backends.Sp2013.Common
{
    public static class SpFeatureHelper
    {
        /// <summary>
        /// activate a feature
        /// </summary>
        /// <param name="features">collection of features</param>
        /// <param name="featureId">feature ID of feature to handle</param>
        /// <param name="force">with or without force</param>
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
        /// activate a feature
        /// </summary>
        /// <param name="features">collection of features</param>
        /// <param name="featureId">feature ID of feature to handle</param>
        /// <param name="force">with or without force</param>
        /// <returns>the activated feature</returns>
        /// <remarks>attention, might throw exception!</remarks>
        internal static SPFeature UpgradeFeatureInFeatureCollection(SPFeatureCollection features, Guid featureId, bool force)
        {
            SPFeature spFeature = null;

            IEnumerable<Exception> upgradeErrors;
            bool success = true;

            string upgradeErrorsAsString = null;

            spFeature = features[featureId];

            var definitionVersion = spFeature.Definition.Version;

            if (spFeature.Version < definitionVersion)
            {
                upgradeErrors = spFeature.Upgrade(force);
            }
            else
            {
                var errMsg = string.Format("Feature '{0}' does not require upgrade.", featureId);

                throw new ApplicationException(errMsg);
            }

            if (upgradeErrors != null && upgradeErrors.Count() > 0)
            {
                success = false;

                foreach (Exception x in upgradeErrors)
                {
                    upgradeErrorsAsString += string.Format(
                        "Error: {0}\n",
                        x.Message
                        );
                }

            }


            if (spFeature.Version != definitionVersion || !success)
            {
                var errMsg = string.Format("Feature upgrade for feature '{0}' failed. Feature version: '{1}', definition version: '{2}'.",
                    featureId,
                    spFeature.Version,
                    definitionVersion);

                if (!force)
                {
                    errMsg += " You might want to try again with 'force' enabled.";
                }

                if (!string.IsNullOrEmpty(upgradeErrorsAsString))
                {
                    errMsg += upgradeErrorsAsString;
                }


                throw new ApplicationException(errMsg);
            }

            return spFeature;
        }

        /// <summary>
        /// to be able to reuse opening of locations in SharePoint and consolidate code, 
        /// a feature action function is required that follows Func: SPFeatureCollection, Guid, bool, SPFeature >
        /// </summary>
        /// <param name="features">collection of features</param>
        /// <param name="featureId">feature ID of feature to handle</param>
        /// <param name="force">with or without force</param>
        /// <returns>always null</returns>
        internal static SPFeature DeactivateFeatureInFeatureCollectionReturnsNull(SPFeatureCollection features, Guid featureId, bool force)
        {
            DeactivateFeatureInFeatureCollection(features, featureId, force);

            return null;
        }

        /// <summary>
        /// deactivate a feature
        /// </summary>
        /// <param name="features">collection of features</param>
        /// <param name="featureId">feature ID of feature to handle</param>
        /// <param name="force">with or without force</param>
        /// <remarks>attention, might throw exception!</remarks>
        private static void DeactivateFeatureInFeatureCollection(SPFeatureCollection features, Guid featureId, bool force)
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


        private static SPFeatureCollection GetFeatureCollection(SPWeb web)
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
