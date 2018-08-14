using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public class ActivatedFeature
    {
        public ActivatedFeature(
                string featureId,
                string locationId,
                string displayName,
                bool faulty,
                Dictionary<string, string> properties,
                DateTime timeActivated,
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
            TimeActivated = timeActivated;
            Version = version;
            DefinitionVersion = definitionVersion;
            FeatureDefinitionScope = featureDefinitionScope;
        }

        [IgnoreDuringEquals]
        public bool CanUpgrade
        {
            get
            {
                return DefinitionVersion > Version;
            }
        }

        public Version DefinitionVersion { get; private set; }

        [IgnoreDuringEquals]
        public string DisplayName { get; private set; }

        [IgnoreDuringEquals]
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
        [IgnoreDuringEquals]
        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();

        [IgnoreDuringEquals]
        public DateTime TimeActivated { get; private set; }

        [IgnoreDuringEquals]
        public Version Version { get; private set; }

        [IgnoreDuringEquals]
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
    }
}
