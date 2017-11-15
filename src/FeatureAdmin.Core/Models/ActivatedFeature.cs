using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    public class ActivatedFeature
    {
        private ActivatedFeature(
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
            FeatureId = featureId;
            LocationId = locationId;
            Definition = definition;
            Faulty = faulty;
            Properties = properties;
            TimeActivated = timeActivated;
            Version = version;
            DefinitionInstallationScope = definitionInstallationScope;
        }


        public Guid FeatureId { get; private set; }

        public Guid LocationId { get; private set; }

        public FeatureDefinition Definition { get; private set; }
        /// <summary>
        /// The scope, where the definition is installed
        /// Url in case of Site, otherwise "Farm"
        /// </summary>
        public string DefinitionInstallationScope { get; private set; }
        public bool Faulty { get; private set; }

        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();

        public DateTime TimeActivated { get; private set; }

        public Version Version { get; private set; }


        public static string GetDefinitionInstallationScope(bool isFeatureDefinitionScopeEqualFarm, string featureParentUrl)
        {
            string definitionInstallationScope = isFeatureDefinitionScopeEqualFarm ?
                "Farm" : featureParentUrl;

            return definitionInstallationScope;
        }

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
