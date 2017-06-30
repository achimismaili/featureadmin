using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.Models.Interfaces
{
    public interface IFeatureDefinition
    {
        List<ActivatedFeature> ActivatedFeatures { get; set; }
        IEnumerable<KeyValuePair<string, string>> AdditionalProperties { get; }
        int CompatibilityLevel { get; }
        SPFeatureDefinition Definition { get; }
        Version DefinitionVersion { get; }
        bool Faulty { get; }
        string GetTitle { get; }
        Guid Id { get; }
        string Name { get; }
        SPFeatureScope Scope { get; }
    }
}