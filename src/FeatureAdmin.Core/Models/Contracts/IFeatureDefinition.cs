using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models.Contracts
{
    public interface IFeatureDefinition 
    {
        Guid DefinitionId { get; }
        
        bool Faulty { get; }
        string Title { get; }
        string Name { get; }
        Scope Scope { get; }
        ICollection<Guid> ActivatedFeatures { get; set; }
        int CompatibilityLevel { get; }
        Version DefinitionVersion { get; }

    }
}