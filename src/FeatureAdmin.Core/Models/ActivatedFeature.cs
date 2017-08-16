using FeatureAdmin.Core.Models.Contracts;
using System;

namespace FeatureAdmin.Core.Models
{
    public class ActivatedFeature : IActivatedFeature 
    {
        public ActivatedFeature(
            Guid featureId,
            Guid locationId,
            int compatibilityLevel,
            string name,
            DateTime timeActivated,
            Version version,
            bool faulty
            )
        {
            ParentFeatureComp = new Tuple<Guid, Guid>(featureId, locationId);
            FeatureId = featureId;
            LocationId = locationId;
            CompatibilityLevel = compatibilityLevel;
            Name = name;
            Faulty = faulty;
        }

        // identifier based on parent, feature-definition and compatibility level
        public Tuple<Guid,Guid> ParentFeatureComp { get; private set; }
        public Guid FeatureId { get; private set; }
        public Guid LocationId { get; private set; }
        public int CompatibilityLevel { get; private set; }
        public string Name { get; private set; }
        public DateTime TimeActivated { get; private set; }
        public Version Version { get; private set; }
        public bool Faulty { get; private set; }
    }
}