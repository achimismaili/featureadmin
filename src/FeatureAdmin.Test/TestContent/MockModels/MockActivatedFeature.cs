using FeatureAdmin.Models.Interfaces;
using System;
using System.Collections.Generic;
using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.Test.TestContent.MockModels
{
    public class MockActivatedFeature : IActivatedFeature
    {
        public SPFeatureDefinition Definition { get; set; }

        public Version DefinitionVersion { get; set; }

        public bool Faulty { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IFeatureParent Parent { get; set; }

        public SPFeatureScope Scope { get; set; }

        public SPFeature SharePointFeature { get; set; }

        public DateTime TimeActivated { get; set; }

        public Version Version { get; set; }
    }
}
