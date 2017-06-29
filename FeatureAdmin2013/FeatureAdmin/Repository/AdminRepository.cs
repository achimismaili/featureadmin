using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Repository
{
    public static class AdminRepository
    {
        public static List<FeatureDefinition> RefreshData()
        {
            InMemoryDatabase.FeatureDefinitions = SharePointFarmService.GetFeatureDefinitions.GetAllFeatureDefinitions();
            InMemoryDatabase.ActivatedFeatures = SharePointFarmService.GetActivatedFeatures.GetAllFromFarm();

            if (InMemoryDatabase.ActivatedFeatures != null && InMemoryDatabase.ActivatedFeatures.Any())
            {
                var distinctActivatedFeatureIds = InMemoryDatabase.ActivatedFeatures.Select(af => af.Id).Distinct();

                foreach (Guid featureId in distinctActivatedFeatureIds)
                {
                    var activatedFeatureGroup = InMemoryDatabase.ActivatedFeatures.Where(af => af.Id == featureId);

                    var featureDef = InMemoryDatabase.FeatureDefinitions.FirstOrDefault(fd => fd.Id == featureId);

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
                        InMemoryDatabase.FeatureDefinitions.Add(newFeatureDef);
                    }
                }
            }

            return InMemoryDatabase.FeatureDefinitions;
        }
    }
}
