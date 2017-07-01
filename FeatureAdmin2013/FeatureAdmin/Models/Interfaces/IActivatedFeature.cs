using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.Models.Interfaces
{
    public interface IActivatedFeature : IFeatureIdentifier
    {
        SPFeatureDefinition Definition { get; }
        Version DefinitionVersion { get; }
        bool Faulty { get; }
        string Name { get; }
        IFeatureParent Parent { get; }
        SPFeature SharePointFeature { get; }
        DateTime TimeActivated { get; }
        Version Version { get; }
    }
}