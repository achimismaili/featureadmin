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
    [Equals]
    [Serializable]
    public class ActivatedFeatureSpecial
    {
        public ActivatedFeatureSpecial(
                ActivatedFeature activatedFeature,
                Location location
            )
        {
            ActivatedFeature = activatedFeature;
            Location = location;
        }

        public ActivatedFeature ActivatedFeature { get; }
        public Location Location { get; }
    }
}
