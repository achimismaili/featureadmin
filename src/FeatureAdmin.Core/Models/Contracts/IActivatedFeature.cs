using System;

namespace FeatureAdmin.Core.Models.Contracts
{
    public interface IActivatedFeature 
    {
        // identifier based on parent, feature-definition and compatibility level
        Tuple<Guid,Guid> ParentFeatureComp { get; }
        Guid FeatureId { get; }
        Guid LocationId { get; }
        int CompatibilityLevel { get; }
        string Name { get; }
        DateTime TimeActivated { get; }
        Version Version { get; }
        bool Faulty { get; }
    }
}