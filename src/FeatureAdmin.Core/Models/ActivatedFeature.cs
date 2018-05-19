using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public class ActivatedFeature : ActivatedFeatureBase, IDisplayableItem
    {
        public ActivatedFeature(
                Guid featureId,
                Guid locationId,
                FeatureDefinition definition,
                bool faulty,
                Dictionary<string, string> properties,
                DateTime timeActivated,
                Version version,
                string sandBoxedSolutionLocation = null
            ) : base(featureId, definition.DisplayName, definition.UniqueIdentifier, locationId, faulty, timeActivated, version,
                definition.Version < version)
        {

            Definition = definition;
            Properties = properties;
        }

        public FeatureDefinition Definition { get; private set; }


        [IgnoreDuringEquals]
        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();


        public List<KeyValuePair<string, string>> GetAsPropertyList()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(nameof(DisplayName),DisplayName),
                new KeyValuePair<string, string>(nameof(FeatureId),FeatureId.ToString()),
                new KeyValuePair<string, string>(nameof(LocationId),LocationId.ToString()),
                new KeyValuePair<string, string>(nameof(Scope), Definition.Scope.ToString()),
                new KeyValuePair<string, string>(nameof(TimeActivated),TimeActivated.ToString()),
                new KeyValuePair<string, string>(nameof(Version),Version.ToString()),
                new KeyValuePair<string, string>(nameof(Faulty),Faulty.ToString()),
                new KeyValuePair<string, string>(nameof(Properties), Common.StringUtilities.PropertiesToString(Properties))
            };
        }
    }
}
