using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Models
{
    public static class ActivatedFeatureFactory
    {
        private static ActivatedFeature MapSpFeatureToActivatedFeature(SPFeature feature, FeatureParent parent)
        {
            var af = ActivatedFeature.GetActivatedFeature(feature, parent);

            return af;
        }

        public static List<ActivatedFeature> MapSpFeatureToActivatedFeature(SPFeatureCollection featureCollection, SPWebService farmWebService)
        {
            var parent = FeatureParent.GetFeatureParent(farmWebService);

            return MapSpFeatureToActivatedFeature(featureCollection, parent);
        }

        public static List<ActivatedFeature> MapSpFeatureToActivatedFeature(SPFeatureCollection featureCollection, SPSite sico)
        {
            var parent = FeatureParent.GetFeatureParent(sico);

            return MapSpFeatureToActivatedFeature(featureCollection, parent);
        }

        public static List<ActivatedFeature> MapSpFeatureToActivatedFeature(SPFeatureCollection featureCollection, SPWeb web)
        {
            var parent = FeatureParent.GetFeatureParent(web);

            return MapSpFeatureToActivatedFeature(featureCollection, parent);
        }

        public static List<ActivatedFeature> MapSpFeatureToActivatedFeature(SPFeatureCollection featureCollection, SPWebApplication webApp)
        {
            var parent = FeatureParent.GetFeatureParent(webApp);

            return MapSpFeatureToActivatedFeature(featureCollection, parent);
        }

        public static List<ActivatedFeature> MapSpFeatureToActivatedFeature(SPFeatureCollection featureCollection, FeatureParent parent)
        {
            List<ActivatedFeature> activatedFeatures = new List<ActivatedFeature>();

            if (featureCollection != null)
            {
                foreach (SPFeature f in featureCollection)
                {
                    var af = MapSpFeatureToActivatedFeature(f, parent);
                    activatedFeatures.Add(af);
                }
            }

            return activatedFeatures;
        }

    }
}
