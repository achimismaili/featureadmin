using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Serializable]
    public class ActivatedFeature : BaseEquatable<ActivatedFeature>
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

        public override string ToString()
        {
            return string.Format(
                "Feature '{0}', id:{1} in location:{2}",
                this.DisplayName,
                this.FeatureId,
                this.LocationId
                );
        }

        protected override bool EqualsInternal(ActivatedFeature right)
        {
            return this.FeatureId == right.FeatureId &&
            this.LocationId == right.LocationId &&
            this.Version == right.Version &&
            this.Faulty == right.Faulty;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        public static bool operator ==(ActivatedFeature left, ActivatedFeature right)
        {
            return object.Equals((object)left, (object)right);
        }

        public static bool operator !=(ActivatedFeature left, ActivatedFeature right)
        {
            return !object.Equals((object)left, (object)right);
        }
    }
}
