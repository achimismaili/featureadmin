using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class ActivatedFeature
    {
        private ActivatedFeature()
        {

        }

        public Guid FeatureId { get; private set; }

        public Guid LocationId { get; private set; }

        public FeatureDefinition Definition { get; private set; }
        public bool Faulty { get; private set; }

        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();

        public DateTime TimeActivated { get; private set; }

        public Version Version { get; private set; }

        public static ActivatedFeature GetActivatedFeature(
                Guid featureId,
                Guid locationId,
                FeatureDefinition definition,
                bool faulty,
                Dictionary<string, string> properties,
                DateTime timeActivated,
                Version version
            )
        {
            var activatedFeature = new ActivatedFeature()
            {
                FeatureId = featureId,
                LocationId = locationId,
                Definition = definition,
                Faulty = faulty,
                Properties = properties,
                TimeActivated = timeActivated,
                Version = version
            };

            return activatedFeature;
        }
    }
}
