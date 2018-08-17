using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Factories
{
    public static class FeatureDefinitionFactory
    {
        ///// <summary>
        ///// Add activated features to list of definitions, when definition does not exist, it gets created
        ///// </summary>
        ///// <param name="existingFeatureDefinitions"></param>
        ///// <param name="featureDefinitionsToAdd"></param>
        ///// <returns></returns>
        ///// <remarks>see also https://stackoverflow.com/questions/12873855/c-sharp-groupby-linq-and-foreach
        ///// </remarks>
        //public static void AddActivatedFeatures(this ICollection<FeatureDefinition> existingFeatureDefinitions, [NotNull] IEnumerable<IGrouping<FeatureDefinition, ActivatedFeature>> featuresToAdd)
        //{
        //    foreach (var featureDefinitionGroup in featuresToAdd)
        //    {
        //        // get feature definition from collection or add a new one if it does not exist yet

        //        var definitionToAddFeaturesTo = existingFeatureDefinitions.FirstOrDefault(fd => fd.Equals(featureDefinitionGroup.Key));

        //        if (definitionToAddFeaturesTo == null)
        //        {
        //            definitionToAddFeaturesTo = featureDefinitionGroup.Key;
        //            existingFeatureDefinitions.Add(definitionToAddFeaturesTo);
        //        }

        //        foreach (ActivatedFeature activeFeature in featureDefinitionGroup)
        //        {
        //            definitionToAddFeaturesTo.ToggleActivatedFeature(activeFeature, true);
        //        }
        //    }
        //}


        public static FeatureDefinition GetFaultyDefinition(
             string uniqueIdentifier,
             Scope scope,
             Version version
            )
        {
            if (string.IsNullOrEmpty(uniqueIdentifier))
            {
                return null;
            }


            Guid featureId;
            int compatibilityLevel;
            string sandBoxedSolutionLocation;


            var splittedId = uniqueIdentifier.Split(Common.Constants.MagicStrings.GuidSeparator);

            if (splittedId.Length >= 1)
            {
                var featureIdAsString = splittedId[0];

                if (!Guid.TryParse(featureIdAsString, out featureId))
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            if (splittedId.Length >= 2)
            {
                var compatibilityLevelAsString = splittedId[1];

                if (!Int32.TryParse(compatibilityLevelAsString, out compatibilityLevel))
                {
                    compatibilityLevel = 0;
                }
            }
            else
            {
                compatibilityLevel = 0;
            }

            if (splittedId.Length >= 3)
            {
                sandBoxedSolutionLocation = splittedId[2];
            }
            else
            {
                sandBoxedSolutionLocation = null;
            }




            var featureDefinition = new FeatureDefinition(
                featureId,
                compatibilityLevel,
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
                sandBoxedSolutionLocation
                );

            return featureDefinition;
        }


        //public static FeatureDefinition GetFeatureDefinition(
        //    ActivatedFeature activatedFeature, Location location)
        //{
        //    if (activatedFeature != null)
        //    {
        //        FeatureDefinition fDef;

        //        if (activatedFeature.Faulty || activatedFeature.Definition == null)
        //        {
        //            fDef = FeatureDefinitionFactory.GetFaultyDefinition(
        //                activatedFeature.FeatureId,
        //                location.Scope,
        //                activatedFeature.Version
        //              );
        //        }
        //        else
        //        {
        //            fDef = activatedFeature.Definition;
        //        }

        //        //fDef.ToggleActivatedFeature(activatedFeature, true);
        //        return fDef;
        //    }
        //    else
        //    {
        //        //TODO Log unexpected definition not found
        //        return null;
        //    }
        //}


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
             string sandBoxedSolutionLocation = null
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
                sandBoxedSolutionLocation);
            return featureDefinition;
        }
    }
}