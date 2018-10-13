using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Serializable]
    public class ActivatedFeature : IEquatable<ActivatedFeature>
    {
        public ActivatedFeature(
                string featureId,
                string locationId,
                string displayName,
                bool faulty,
                Dictionary<string, string> properties,
                DateTime? timeActivated,
                Version version,
                Version definitionVersion,
                FeatureDefinitionScope featureDefinitionScope = FeatureDefinitionScope.Farm
            )
        {
            FeatureId = featureId;
            LocationId = locationId;
            DisplayName = displayName;
            Faulty = faulty;
            Properties = properties;
            if (timeActivated.HasValue)
            {
                TimeActivated = timeActivated.Value;
            }
            else
            {
                TimeActivated = new DateTime(1900, 1, 1);
            }
            Version = version;
            DefinitionVersion = definitionVersion;
            FeatureDefinitionScope = featureDefinitionScope;
        }

        public bool CanUpgrade
        {
            get
            {
                return DefinitionVersion > Version;
            }
        }

        public Version DefinitionVersion { get; private set; }

        public string DisplayName { get; private set; }

        public bool Faulty { get; private set; }


        /// <summary>
        /// This is the UniqueIdentifier of the FeatureDefinition
        /// </summary>
        /// <remarks>
        /// The feature id is not sufficient, because not unique, 
        /// e.g. sandboxed solutions (and add ins?) can have multiple definition instances with each same feature id
        /// also same feature definition can be deployed to multiple compatibility levels
        /// This ID is relevant for 'equal'
        /// </remarks>
        public string FeatureId { get; private set; }

        public Guid FeatureGuid
        {
            get
            {
                return Common.StringHelper.UniqueIdToGuid(FeatureId);
            }
        }

        /// <summary>
        /// This is the UniqueId of the location
        /// </summary>
        /// <remarks>
        /// The location id is not sufficient, because not unique, 
        /// e.g. multiple site colleciton restores can have same web id
        /// This ID is relevant for 'equal'
        /// </remarks>
        public string LocationId { get; private set; }

        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();

        public DateTime TimeActivated { get; private set; }

        public Version Version { get; private set; }

        public FeatureDefinitionScope FeatureDefinitionScope { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ActivatedFeature);
        }

        public bool Equals(ActivatedFeature other)
        {
            return other != null &&
                   Faulty == other.Faulty &&
                   FeatureId == other.FeatureId &&
                   LocationId == other.LocationId &&
                   EqualityComparer<Version>.Default.Equals(Version, other.Version);
        }

        public override int GetHashCode()
        {
            var hashCode = 2029977652;
            hashCode = hashCode * -1521134295 + Faulty.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FeatureId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LocationId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Version>.Default.GetHashCode(Version);
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format(
                "Feature '{0}', id:{1} in location:{2}",
                this.DisplayName,
                this.FeatureId,
                this.LocationId
                );
        }

        public static bool operator ==(ActivatedFeature feature1, ActivatedFeature feature2)
        {
            return EqualityComparer<ActivatedFeature>.Default.Equals(feature1, feature2);
        }

        public static bool operator !=(ActivatedFeature feature1, ActivatedFeature feature2)
        {
            return !(feature1 == feature2);
        }
    }
}
