using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class FeatureDefinitionFactory : BaseItem<ActivatedFeature>
    {
        

       
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
                    fDef = FeatureDefinitionFactory.GetFaultyDefinition(
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

                fDef.ToggleActivatedFeature(activatedFeature, true);
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
    }
}