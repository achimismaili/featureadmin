using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.Models.Interfaces
{
    public interface IActivatedFeature
    {
        SPFeatureDefinition Definition { get; }
        Version DefinitionVersion { get; }
        bool Faulty { get; }
        Guid Id { get; }
        string Name { get; }
        IFeatureParent Parent { get; }
        SPFeatureScope Scope { get; }
        SPFeature SharePointFeature { get; }
        DateTime TimeActivated { get; }
        Version Version { get; }
    }
}