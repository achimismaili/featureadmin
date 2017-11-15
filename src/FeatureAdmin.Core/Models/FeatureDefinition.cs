using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    public class FeatureDefinition : BaseItem<Guid>
    {
        private FeatureDefinition(
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
            string definitionInstallationScope = "Farm"
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
        public IReadOnlyCollection<Guid> ActivatedFeatures
        {
            get
            {
                return activatedFeatures.AsReadOnly();
            }
        }

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
        [IgnoreDuringEquals]
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

        public static FeatureDefinition GetFaultyDefinition(
             Guid id,
             Scope scope,
             Version version,
             string definitionInstallationScope
            )
        {
            var featureDefinition = new FeatureDefinition(id,
                0,
                "Faulty, orphaned feature - no feature definition available",
                "Faulty, orphaned feature",
                false,
                "Faulty, orphaned feature",
                null,
                scope,
                "Faulty, orphaned feature",
                Guid.Empty,
                "n/a",
                version,
                definitionInstallationScope);

            return featureDefinition;
        }


        public static FeatureDefinition GetFeatureDefinition(
            ActivatedFeature activatedFeature, Location location)
        {
            if (activatedFeature != null)
            {
                FeatureDefinition fDef;

                if (activatedFeature.Faulty || activatedFeature.Definition == null)
                {
                    fDef = FeatureDefinition.GetFaultyDefinition(
                        activatedFeature.FeatureId,
                        location.Scope,
                        activatedFeature.Version,
                        activatedFeature.DefinitionInstallationScope
                      );
                }
                else
                {
                    fDef = activatedFeature.Definition;
                }

                fDef.ToggleActivatedFeature(activatedFeature.LocationId, true);
                return fDef;
            }
            else
            {
                //TODO Log unexpected definition not found
                return null;
            }
        }


        public static FeatureDefinition GetFeatureDefinition(
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
             string definitionInstallationScope = "Farm"
            )
        {
            var featureDefinition = new FeatureDefinition(
                id,
                compatibilityLevel,
                description,
                displayName,
                hidden,
                name,
                properties,
                scope,
                title,
                solutionId,
                uIVersion,
                version,
                definitionInstallationScope);
            return featureDefinition;
        }

        public static string GetDefinitionInstallationScope(bool isFeatureDefinitionScopeEqualFarm, string featureParentUrl)
        {
            return ActivatedFeature.GetDefinitionInstallationScope(isFeatureDefinitionScopeEqualFarm, featureParentUrl);
        }
    }
}