using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Factories
{
    [Equals]
    public class ActivatedFeatureFactory
    {
        public static ActivatedFeature GetActivatedFeature(
                Guid featureId,
                Guid locationId,
                FeatureDefinition definition,
                bool faulty,
                Dictionary<string, string> properties,
                DateTime timeActivated,
                Version version,
                FeatureDefinitionScope featureDefinitionScope = FeatureDefinitionScope.Farm
            )
        {
            var activatedFeature = new ActivatedFeature(
                featureId,
                locationId,
                definition,
                faulty,
                properties,
                timeActivated,
                version,
                featureDefinitionScope);

            return activatedFeature;
        }
    }
}
