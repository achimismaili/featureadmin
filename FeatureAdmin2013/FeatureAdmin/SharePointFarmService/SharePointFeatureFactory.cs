using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService
{
    public static class SharePointFeatureFactory
    {
        public List<ActivatedFeature> GetAllActivatedFeaturesFromFarm()
        {
            var allActivatedFeatures = new List<ActivatedFeature>();

            // Get Farm features
            var farm = new Farm();


            return allActivatedFeatures;
        }
    }
}
