using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public class FeatureDefinition : BaseItem
    {
        public FeatureDefinition(
            Guid id,
             int compatibilityLevel,
             string description,
             string displayName,
             bool hidden,
             string name,
             Dictionary<string, string> properties,
             Scope scope,
             string title,
             Guid solutionId,
             string uIVersion,
             Version version,
            string definitionInstallationScope = Common.Constants.Defaults.DefinitionInstallationScopeFarm
            ) : base()
        {
            Id = id;
            CompatibilityLevel = compatibilityLevel;
            Description = description == null ? string.Empty : description;
            DisplayName = displayName == null ? string.Empty : displayName;
            Hidden = hidden;
            Name = name == null ? string.Empty : name;
            Properties = properties == null ? new Dictionary<string, string>() : properties;
            Scope = scope;
            Title = title == null ? string.Empty : title;
            SolutionId = solutionId;
            UIVersion = uIVersion == null ? string.Empty : uIVersion;
            Version = version;
            DefinitionInstallationScope = definitionInstallationScope;
        }

        public int CompatibilityLevel { get; private set; }
        [IgnoreDuringEquals]
        public string Description { get; private set; }


        [IgnoreDuringEquals]
        public int Faulty { get; private set; }

        [IgnoreDuringEquals]
        public int CanUpgrade { get; private set; }

        [IgnoreDuringEquals]
        public bool Hidden { get; private set; }

        /// <summary>
        /// The Scope, in which this Definition is installed
        /// </summary>
        /// <remarks>
        /// All Farm Solutions have Scope FARM
        /// Sandboxed Solutions have Scope Site
        /// </remarks>
        public string DefinitionInstallationScope { get; private set; }
        
        public string Name { get; private set; }
        [IgnoreDuringEquals]
        public Dictionary<string, string> Properties { get; private set; }
        [IgnoreDuringEquals]
        public Guid SolutionId { get; private set; }
        [IgnoreDuringEquals]
        public string Title { get; private set; }
        [IgnoreDuringEquals]
        public string UIVersion { get; private set; }
        [IgnoreDuringEquals]
        public Version Version { get; private set; }


        public override List<KeyValuePair<string, string>> GetAsPropertyList()
        {
            var propList = new List<KeyValuePair<string, string>>();

            propList.Add(new KeyValuePair<string, string>(nameof(Title), Title));
            propList.Add(new KeyValuePair<string, string>(nameof(Name), Name));
            propList.Add(new KeyValuePair<string, string>(nameof(CompatibilityLevel), CompatibilityLevel.ToString()));
            propList.Add(new KeyValuePair<string, string>(nameof(Description), Description));
            propList.Add(new KeyValuePair<string, string>(nameof(Hidden), Hidden.ToString()));
            propList.Add(new KeyValuePair<string, string>(nameof(SolutionId), SolutionId.ToString()));
            propList.Add(new KeyValuePair<string, string>(nameof(UIVersion), UIVersion.ToString()));
            propList.Add(new KeyValuePair<string, string>(nameof(Version), Version == null ? string.Empty : Version.ToString()));
            propList.Add(new KeyValuePair<string, string>(nameof(DefinitionInstallationScope), DefinitionInstallationScope));
            propList.Add(new KeyValuePair<string, string>(nameof(Properties), Common.StringUtilities.PropertiesToString(Properties)));

            propList.AddRange(GetAsPropertyList(true));

            return propList;
        }
    }
}