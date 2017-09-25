using FeatureAdmin.Core.Models.Contracts;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class FeatureDefinition : IFeatureDefinition
    {
        private FeatureDefinition()
        {
            Properties = new Dictionary<string, string>();
        }

        public Guid Id { get; private set; }
        public int CompatibilityLevel { get; private set; }
        public string Description { get; private set; }
        public string DisplayName { get; private set; }
        public bool Faulty { get; private set; }
        public bool Hidden { get; private set; }
        public string Name { get; private set; }
        public Dictionary<string, string> Properties { get; private set; }
        public Scope Scope { get; private set; }
        public Guid SolutionId { get; private set; }
        public string Title { get; private set; }
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