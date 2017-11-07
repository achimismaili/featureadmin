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
             bool faulty,
             bool hidden,
             string name,
             Dictionary<string, string> properties,
             Scope scope,
             string title,
             Guid solutionId,
             string uIVersion,
             Version version
            ) : base()
        {
            Id = id;
                    CompatibilityLevel = compatibilityLevel;
                    Description = description == null ? string.Empty : description;
                    DisplayName = displayName == null ? string.Empty : displayName;
                    Faulty = faulty;
                    Hidden = hidden;
                    Name = name == null ? string.Empty : name;
                    Properties = properties == null ? new Dictionary<string, string>() : properties;
            Scope = scope;
                    Title = title == null ? string.Empty : title;
                    SolutionId = solutionId;
                    UIVersion = uIVersion == null ? string.Empty : uIVersion;
            Version = version;
        }

        public int CompatibilityLevel { get; private set; }
        public string Description { get; private set; }

        [IgnoreDuringEquals]
        public IReadOnlyCollection<Guid> ActivatedFeatures
        {
            get
            {
                return activatedFeatures.AsReadOnly();
            }
        }

        public bool Faulty { get; private set; }
        public bool Hidden { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string, string> Properties { get; private set; }
        public Guid SolutionId { get; private set; }
        public string Title { get; private set; }

        public string UIVersion { get; private set; }
        public Version Version { get; private set; }

        public static FeatureDefinition GetFaultyDefinition(
             Guid id,
             Scope scope,
             Version version
            )
        {
            var featureDefinition = new FeatureDefinition(id,
                0,
                "Faulty, orphaned feature - no feature definition available",
                "Faulty, orphaned feature",
                true,
                false,
                "Faulty, orphaned feature",
                null,
                scope,
                "Faulty, orphaned feature",
                Guid.Empty,
                "n/a",
                version);

            return featureDefinition;
        }


        public static FeatureDefinition GetFeatureDefinition(
            ActivatedFeature activatedFeature, Scope scope)
        {
            if (activatedFeature != null)
            {
                FeatureDefinition fDef;

                if (activatedFeature.Faulty || activatedFeature.Definition == null)
                {
                    fDef = FeatureDefinition.GetFaultyDefinition(
                        activatedFeature.FeatureId,
                        scope,
                        activatedFeature.Version
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
             bool faulty,
             bool hidden,
             string name,
             Dictionary<string, string> properties,
             Scope scope,
             string title,
             Guid solutionId,
             string uIVersion,
             Version version
            )
        {
            var featureDefinition = new FeatureDefinition(
                id,
                compatibilityLevel,
                description,
                displayName,
                faulty,
                hidden,
                name,
                properties,
                scope,
                title,
                solutionId,
                uIVersion,
                version);
            return featureDefinition;
        }
    }
}