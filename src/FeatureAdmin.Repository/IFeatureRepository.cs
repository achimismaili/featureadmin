using FeatureAdmin.Core.Models;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Repository
{
    public interface IFeatureRepository 
    {
        //IEnumerable<ActivatedFeature> SearchActivatedFeatures();
        IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchString);
        //IEnumerable<Location> SearchLocations();

        //void AddActivatedFeature(ActivatedFeature feature);
        void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions);
        //void RemoveActivatedFeature(ActivatedFeature feature);

        void Clear();
        


    }
}
