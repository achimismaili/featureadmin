using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
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
                string definitionInstallationScope = "Farm"
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
                definitionInstallationScope);

            return activatedFeature;
        }
    }
}
