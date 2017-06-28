using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SharePointFarmService
{
    public static class FeatureService
    {
        public static List<ActivatedFeature> GetAllActivatedFeaturesFromFarm()
        {
            var allActivatedFeatures = new List<ActivatedFeature>();

            // Get Farm features
            var farm = new Farm();
            farm.GetAllActivatedFeatures();


            return allActivatedFeatures;
        }
    }
}
