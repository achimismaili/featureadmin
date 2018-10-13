using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Serializable]
    public class FeatureDefinition : IEquatable<FeatureDefinition>
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
            string sandBoxedSolutionLocationId = null
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
            SandBoxedSolutionLocation = sandBoxedSolutionLocationId;
            UniqueIdentifier = Common.StringHelper.GenerateUniqueId(id, compatibilityLevel, sandBoxedSolutionLocationId);
        }

        public int CompatibilityLevel { get; private set; }

        public string Description { get; private set; }

        public string DisplayName { get; protected set; }

        public int Faulty { get; private set; }
        public bool Hidden { get; private set; }
        public Guid Id { get; protected set; }

        public string Name { get; private set; }

        public Dictionary<string, string> Properties { get; private set; }

        /// <summary>
        /// The location Id, if this is a sandboxed solution feature definition
        /// </summary>
        /// <remarks>
        /// All Farm Solutions have value null
        /// Sandboxed Solutions have the locationId of the site collection
        /// </remarks>
        public string SandBoxedSolutionLocation { get; private set; }

        public Scope Scope { get; protected set; }

       public Guid SolutionId { get; private set; }

        public string Title { get; private set; }

        public string UIVersion { get; private set; }

        public string UniqueIdentifier { get; private set; }
        public Version Version { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as FeatureDefinition);
        }

        public bool Equals(FeatureDefinition other)
        {
            return other != null &&
                   DisplayName == other.DisplayName &&
                   Faulty == other.Faulty &&
                   Hidden == other.Hidden &&
                   Name == other.Name &&
                   Scope == other.Scope &&
                   SolutionId.Equals(other.SolutionId) &&
                   Title == other.Title &&
                   UIVersion == other.UIVersion &&
                   UniqueIdentifier == other.UniqueIdentifier &&
                   EqualityComparer<Version>.Default.Equals(Version, other.Version);
        }

        public override int GetHashCode()
        {
            var hashCode = 632039311;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + Faulty.GetHashCode();
            hashCode = hashCode * -1521134295 + Hidden.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Scope.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(SolutionId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UIVersion);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UniqueIdentifier);
            hashCode = hashCode * -1521134295 + EqualityComparer<Version>.Default.GetHashCode(Version);
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1},Id:'{2}'\n{3}",
                this.Scope,
                this.DisplayName,
                this.Id,
                this.Description
                );
        }

        public static bool operator ==(FeatureDefinition definition1, FeatureDefinition definition2)
        {
            return EqualityComparer<FeatureDefinition>.Default.Equals(definition1, definition2);
        }

        public static bool operator !=(FeatureDefinition definition1, FeatureDefinition definition2)
        {
            return !(definition1 == definition2);
        }
    }
}