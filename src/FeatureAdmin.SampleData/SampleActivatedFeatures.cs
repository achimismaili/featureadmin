using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SampleData
{
    public static class SampleActivatedFeatures
    {
        public static List<ActivatedFeature> GetActivatedFeatures(IEnumerable<ILocation> locations)
        {
            var featureList = new List<ActivatedFeature>();

            if (locations == null)
            {
                return featureList;
            }

            foreach (Location l in locations)
            {
                List<FeatureDefinition> featureDefinitions;

                switch (l.Scope)
                {
                    //case Core.Models.Enums.Scope.Web:
                    //    featureDefinitions = StandardFeatureDefinitions.GetWebFeatureDefinitions();
                    //    break;
                    case Core.Models.Enums.Scope.Site:
                        featureDefinitions = StandardFeatureDefinitions.GetSiteFeatureDefinitions();
                        // featureDefinitions = StandardFeatureDefinitions.GetSiteWorkflowFeatureDefinitions();
                        break;
                    case Core.Models.Enums.Scope.WebApplication:
                        featureDefinitions = StandardFeatureDefinitions.GetWebAppFeatureDefinitions();
                        break;
                    case Core.Models.Enums.Scope.Farm:
                        featureDefinitions = StandardFeatureDefinitions.GetFarmFeatureDefinitions();
                        break;
                    default:
                        featureDefinitions = StandardFeatureDefinitions.GetWebFeatureDefinitions();
                        break;
                }

                foreach (FeatureDefinition fd in featureDefinitions)
                {
                    // only show 80% of all features as activated
                    Random rand = new Random();
                    if (rand.Next(1, 101) <= 80)
                    {
                        var feature = ActivatedFeature.GetActivatedFeature(
                            fd.Id, l.Id, false, null, DateTime.Now.AddMonths(-6).AddDays(-15), fd.Version);
                        featureList.Add(feature);
                    }
                }
            }

            return featureList;
        }
    }
}
