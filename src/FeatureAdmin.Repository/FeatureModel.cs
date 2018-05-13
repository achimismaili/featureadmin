using FeatureAdmin.Core.Models;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Repository
{
    public class FeatureModel : Model 
    {
        private List<ActivatedFeature> ActivatedFeatures;
        private List<FeatureDefinition> FeatureDefinitions;
        private Dictionary<Guid, Location> Locations;

        public FeatureModel()
        {
            ActivatedFeatures = new List<ActivatedFeature>();
            FeatureDefinitions = new List<FeatureDefinition>();
            Locations = new Dictionary<Guid, Location>();
        }

        public void AddFeatureDefinitions(IEnumerable<FeatureDefinition> farmFeatureDefinitions)
        {
            if (farmFeatureDefinitions != null)
            {
                FeatureDefinitions.AddRange(farmFeatureDefinitions);

            }
        }

        public void Clear()
        {
            ActivatedFeatures.Clear();
            FeatureDefinitions.Clear();
            Locations.Clear();
        }

        public IEnumerable<FeatureDefinition> SearchFeatureDefinitions()
        {
            return FeatureDefinitions;
        }
    }
}
