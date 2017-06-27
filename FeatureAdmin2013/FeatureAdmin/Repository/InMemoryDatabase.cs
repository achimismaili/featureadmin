using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Repository
{
    public static class InMemoryDatabase
    {
        static List<ActivatedFeature> ActivatedFeatures;
        static List<FeatureDefinition> FeatureDefinitions;

        static InMemoryDatabase()
        {
            ActivatedFeatures = ActivatedFeaturesRepository.Refresh;
            FeatureDefinitions = FeatureDefinitionRepository.Refresh; 
        }


    }
}
