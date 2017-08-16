using FeatureAdmin.Models.Interfaces;
using System;
using System.Collections.Generic;
using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.Test.TestContent.MockModels
{
    public class MockFeatureDefinition : IFeatureDefinition
    {
        public List<ActivatedFeature> ActivatedFeatures { get; set; }
      
        public IEnumerable<KeyValuePair<string, string>> AdditionalProperties { get; set; }

        public int CompatibilityLevel { get; set; }

        public SPFeatureDefinition Definition { get; set; }

        public Version DefinitionVersion { get; set; }

        public bool Faulty{ get; set; }

        public string GetTitle { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public SPFeatureScope Scope { get; set; }
    }
}
