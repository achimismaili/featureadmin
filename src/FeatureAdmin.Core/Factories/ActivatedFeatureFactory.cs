using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Factories
{

    public class ActivatedFeatureFactory
    {
        public static ActivatedFeature GetActivatedFeature(
                string featureId,
                string locationId,
                string displayName,
                bool faulty,
                Dictionary<string, string> properties,
                DateTime? timeActivated,
                Version version,
                Version definitionVersion,
                FeatureDefinitionScope featureDefinitionScope = FeatureDefinitionScope.Farm
            )
        {
            var activatedFeature = new ActivatedFeature(
                featureId,
                locationId,
                displayName,
                faulty,
                properties,
                timeActivated,
                version,
                definitionVersion,
                featureDefinitionScope);

            return activatedFeature;
        }
    }



}
