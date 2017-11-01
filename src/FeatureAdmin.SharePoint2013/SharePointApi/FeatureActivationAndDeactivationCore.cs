using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePoint2013.SharePointApi
{
    public static class FeatureActivationAndDeactivationCore
    {
        public static int ProcessFarmFeatureInFarm(SPWebService farm, Guid featureId, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var featuresModifiedCounter = 0;

            if (farm == null)
            {
                exception = new ArgumentNullException("farm was null");
                return featuresModifiedCounter;
            }


            featuresModifiedCounter = ProcessSingleFeatureInFeatureCollection(farm.Id, farm.Features, featureId, activate, force, out exception);

            return featuresModifiedCounter;

        }

        public static int ProcessWebAppFeatureInWebApp(SPWebApplication webApp, Guid featureId, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var featuresModifiedCounter = 0;

            if (webApp == null)
            {
                exception = new ArgumentNullException("web app was null");
                return featuresModifiedCounter;
            }

            return ProcessSingleFeatureInFeatureCollection(webApp.Id, webApp.Features, featureId, activate, force, out exception);
        }

        public static int ProcessSiteFeatureInSite(SPSite site, Guid featureId, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var featuresModifiedCounter = 0;

            if (site == null)
            {
                exception = new ArgumentNullException("site was null");
                return featuresModifiedCounter;
            }

            return ProcessSingleFeatureInFeatureCollection(site.ID, site.Features, featureId, activate, force, out exception);
        }

        public static int ProcessWebFeatureInWeb(SPWeb web, Guid featureId, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var featuresModifiedCounter = 0;

            if (web == null)
            {
                exception = new ArgumentNullException("web was null");
                return featuresModifiedCounter;
            }

            return ProcessSingleFeatureInFeatureCollection(web.ID, web.Features, featureId, activate, force, out exception);
        }

        private static int ProcessSingleFeatureInFeatureCollection(Guid parentId, SPFeatureCollection features, Guid featureId, bool activate, bool force, out Exception exception)
        {
            exception = null;
            var featuresModifiedCounter = 0;

            if (features == null)
            {
                exception = new ArgumentNullException("feature collection was null");
                return featuresModifiedCounter;
            }

            try
            {
                if (activate)
                {
                    // activate feature
                    var feature = features.Add(featureId, force);
                    if (feature != null)
                    {
                        featuresModifiedCounter++;
                    }
                }
                else
                {
                    // deactivate feature
                    var featuresActiveBefore = features.Count();

                    features.Remove(featureId, force);
                    if (featuresActiveBefore > features.Count)
                    {
                        featuresModifiedCounter++;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return featuresModifiedCounter;
        }
    }
}
