using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public class ActivatedFeature 
    {
        public ActivatedFeature(
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
            FeatureId = featureId;
            FeatureDefinitionUniqueIdentifier = definition.UniqueIdentifier;
            LocationId = locationId;
            Definition = definition;
            Faulty = faulty;
            Properties = properties;
            TimeActivated = timeActivated;
            Version = version;
            CanUpgrade = definition.Version < version;
            FeatureDefinitionScope = featureDefinitionScope;
        }

        [IgnoreDuringEquals]
        public bool CanUpgrade { get; private set; }

        public FeatureDefinition Definition { get; private set; }

        [IgnoreDuringEquals]
        public string DisplayName { get {
                return Definition.DisplayName;
            } }

        [IgnoreDuringEquals]
        public bool Faulty { get; private set; }

        public string FeatureDefinitionUniqueIdentifier { get; private set; }

        // only feature id and location are relevant for equal
        public Guid FeatureId { get; private set; }

        // only feature id and location are relevant for equal
        public Guid LocationId { get; private set; }
        [IgnoreDuringEquals]
        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();

        [IgnoreDuringEquals]
        public DateTime TimeActivated { get; private set; }

        [IgnoreDuringEquals]
        public Version Version { get; private set; }

        [IgnoreDuringEquals]
        public FeatureDefinitionScope FeatureDefinitionScope { get; private set; }
    }
}
