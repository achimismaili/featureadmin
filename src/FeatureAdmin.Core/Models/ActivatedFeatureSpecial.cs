using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    /// <summary>
    /// Special activated feature class for upgradable and faulty features
    /// </summary>
    /// <remarks>
    /// Used to return search results for Feature Cleanup and Feature Upgrade
    /// </remarks>
    [Serializable]
    public class ActivatedFeatureSpecial
    {
        public ActivatedFeatureSpecial(
                ActivatedFeature activatedFeature,
                FeatureDefinition definition,
                Location location
            )
        {
            ActivatedFeature = activatedFeature;
            Definition = definition;
            Location = location;
        }

        public ActivatedFeature ActivatedFeature { get; }
        public FeatureDefinition Definition { get; }
        public Location Location { get; }
    }
}
