using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    public abstract class ActivatedFeatureBase
    {
        public ActivatedFeatureBase(
                Guid featureId,
                string displayName,
                string featureDefinitionUniqueIdentifier,
                Guid locationId,
                bool faulty,
                DateTime timeActivated,
                Version version,
                bool canUpgrade
            )
        {
            FeatureId = featureId;
            displayName = DisplayName;
            FeatureDefinitionUniqueIdentifier = featureDefinitionUniqueIdentifier;
            LocationId = locationId;
            Faulty = faulty;
            TimeActivated = timeActivated;
            Version = version;
            CanUpgrade = canUpgrade;
        }

        [IgnoreDuringEquals]
        public string DisplayName { get; private set; }
        [IgnoreDuringEquals]
        public bool Faulty { get; private set; }

        // only feature id and location are relevant for equal
        public Guid FeatureId { get; private set; }

        // only feature id and location are relevant for equal
        public Guid LocationId { get; private set; }

        public string FeatureDefinitionUniqueIdentifier { get; private set; }

        [IgnoreDuringEquals]
        public DateTime TimeActivated { get; private set; }
        [IgnoreDuringEquals]
        public Version Version { get; private set; }

        [IgnoreDuringEquals]
        public bool CanUpgrade { get; private set; }
    }
}
