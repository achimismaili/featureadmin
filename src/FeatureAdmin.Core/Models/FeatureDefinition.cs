using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class FeatureDefinition : BaseItem
    { 
        private FeatureDefinition()
        {
            Properties = new Dictionary<string, string>();
            ActivatedFeatures = new List<ActivatedFeature>().AsReadOnly();
        }

        
        public IReadOnlyCollection<ActivatedFeature> ActivatedFeatures { get; protected set; }
        public int CompatibilityLevel { get; private set; }
        public string Description { get; private set; }
        
        public bool Faulty { get; private set; }
        public bool Hidden { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string, string> Properties { get; private set; }
        public Guid SolutionId { get; private set; }
        public string Title { get; private set; }

        /// <summary>
        /// adds or removes an activated feature to the list of activated features
        /// </summary>
        /// <param name="featureDefinition">feature definition, which will be returned with changed feature-list</param>
        /// <param name="activatedFeature">feature to add or remove</param>
        /// <param name="add">adds if true, removes if false</param>
        /// <returns>feature definition with changed activatedfeatures list</returns>
        public static FeatureDefinition ToggleActivatedFeature(FeatureDefinition featureDefinition, ActivatedFeature activatedFeature, bool add)
        {
            
            var activatedFeatures = new List<ActivatedFeature>(featureDefinition.ActivatedFeatures);

            if (add)
            {
                activatedFeatures.Add(activatedFeature);
            }
            else
            {
                activatedFeatures.Remove(activatedFeature);
            }

            featureDefinition.ActivatedFeatures = activatedFeatures.AsReadOnly();
            return featureDefinition;
        }
        public string UIVersion { get; private set; }
        public Version Version { get; private set; }

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
            var featureDefinition = new FeatureDefinition()
            {
                Id = id,
                CompatibilityLevel = compatibilityLevel,
                Description = description,
                DisplayName = displayName,
                Faulty = faulty,
                Hidden = hidden,
                Name = name,
                Properties = properties,
                Scope = scope,
                Title = title,
                SolutionId = solutionId,
                UIVersion = uIVersion,
                Version = version
            };
            return featureDefinition;
        }

    }
}