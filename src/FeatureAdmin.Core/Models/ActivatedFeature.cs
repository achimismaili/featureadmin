using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    public class ActivatedFeature : IDisplayableItem
    {
        [IgnoreDuringEquals]
        public string DisplayName
        {
            get
            {
                if (Definition == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Definition.DisplayName;
                }
            }
        }

        public ActivatedFeature(
                Guid featureId,
                Guid locationId,
                FeatureDefinition definition,
                bool faulty,
                Dictionary<string, string> properties,
                DateTime timeActivated,
                Version version,
                string definitionInstallationScope = Common.Constants.Defaults.DefinitionInstallationScopeFarm
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
        [IgnoreDuringEquals]
        public FeatureDefinition Definition { get; private set; }

        [IgnoreDuringEquals]
        /// <summary>
        /// The scope, where the definition is installed
        /// Url in case of Site, otherwise "Farm"
        /// </summary>
        public string DefinitionInstallationScope { get; private set; }
        [IgnoreDuringEquals]
        public bool Faulty { get; private set; }
        [IgnoreDuringEquals]
        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();
        [IgnoreDuringEquals]
        public DateTime TimeActivated { get; private set; }
        [IgnoreDuringEquals]
        public Version Version { get; private set; }

        
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
                new KeyValuePair<string, string>(nameof(DefinitionInstallationScope), DefinitionInstallationScope),
            new KeyValuePair<string, string>(nameof(Properties), Common.StringUtilities.PropertiesToString(Properties))
        };
        }
    }
}
