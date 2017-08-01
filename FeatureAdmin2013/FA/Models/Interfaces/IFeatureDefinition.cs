using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FA.Models.Interfaces
{
    public interface IFeatureDefinition : IFeatureIdentifier
    {
        List<ActivatedFeature> ActivatedFeatures { get; set; }
        IEnumerable<KeyValuePair<string, string>> AdditionalProperties { get; }
        int CompatibilityLevel { get; }
        SPFeatureDefinition Definition { get; }
        Version DefinitionVersion { get; }
        bool Faulty { get; }
        string GetTitle { get; }
        string Name { get; }
    }
}