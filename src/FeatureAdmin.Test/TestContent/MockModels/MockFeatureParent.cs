using FeatureAdmin.Models.Interfaces;
using System;
using System.Collections.Generic;
using FeatureAdmin.Models;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.Test.TestContent.MockModels
{
    public class MockFeatureParent : IFeatureParent
    {
        public string DisplayName { get; set; }

        public Guid Id { get; set; }

        public SPFeatureScope Scope { get; set; }

        public string Url { get; set; }
    }
}
