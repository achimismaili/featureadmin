using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public class FeatureDefinition 
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
            string sandBoxedSolutionLocation = null
            ) 
        {
            Id = id;
            CompatibilityLevel = compatibilityLevel;
            Description = description;
            DisplayName = displayName == null ? string.Empty : displayName;
            Hidden = hidden;
            Name = name == null ? string.Empty : name;
            Properties = properties == null ? new Dictionary<string, string>() : properties;
            Scope = scope;
            Title = title == null ? string.Empty : title;
            SolutionId = solutionId;
            UIVersion = uIVersion == null ? string.Empty : uIVersion;
            Version = version;
            SandBoxedSolutionLocation = sandBoxedSolutionLocation;
            UniqueIdentifier = string.Format(
                "{0}-{1}{2}",
                id.ToString(),
                compatibilityLevel,
                sandBoxedSolutionLocation == null ? string.Empty : "-" + sandBoxedSolutionLocation
                );
        }

        public int CanUpgrade { get; private set; }
        public int CompatibilityLevel { get; private set; }
        [IgnoreDuringEquals]
        public string Description { get; private set; }

        public string DisplayName { get; protected set; }

        public int Faulty { get; private set; }
        public bool Hidden { get; private set; }
        public Guid Id { get; protected set; }

        public string Name { get; private set; }

        [IgnoreDuringEquals]
        public Dictionary<string, string> Properties { get; private set; }

        /// <summary>
        /// The location url, if this is a sandboxed solution feature definition
        /// </summary>
        /// <remarks>
        /// All Farm Solutions have value null
        /// Sandboxed Solutions have the url
        /// </remarks>
        public string SandBoxedSolutionLocation { get; private set; }

        public Scope Scope { get; protected set; }

        [IgnoreDuringEquals]
        public Guid SolutionId { get; private set; }

        [IgnoreDuringEquals]
        public string Title { get; private set; }

        [IgnoreDuringEquals]
        public string UIVersion { get; private set; }

        public string UniqueIdentifier { get; private set; }
        public Version Version { get; private set; }
        public List<KeyValuePair<string, string>> GetAsPropertyList()
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
            propList.Add(new KeyValuePair<string, string>(nameof(SandBoxedSolutionLocation), SandBoxedSolutionLocation ));
            propList.Add(new KeyValuePair<string, string>(nameof(Properties), Common.StringUtilities.PropertiesToString(Properties)));

            return propList;
        }
    }
}