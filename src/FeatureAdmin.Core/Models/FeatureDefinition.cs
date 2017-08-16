using FeatureAdmin.Core.Models.Contracts;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class FeatureDefinition : IFeatureDefinition
    {
        public FeatureDefinition(
            Guid definitionId,
            string title,
            string name,
            Scope scope,
            int compatibilityLevel,
            Version definitionVersion,
            bool faulty
            )
        {
            DefinitionId = definitionId;
            Title = title;
            Name = name;
            Scope = scope;
            CompatibilityLevel = compatibilityLevel;
            DefinitionVersion = DefinitionVersion;
            Faulty = faulty;
        }

        public Guid DefinitionId { get; private set; }
        public string Title { get; private set; }
        public string Name { get; private set; }
        public Scope Scope { get; private set; }
        public ICollection<Guid> ActivatedFeatures { get; set; }
        public int CompatibilityLevel { get; private set; }
        public Version DefinitionVersion { get; private set; }
        public bool Faulty { get; private set; }
    }
}