using System;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace FA.Models.Interfaces
{
    public interface IFeatureParent
    {
        string DisplayName { get; }
        Guid Id { get; }
        SPFeatureScope Scope { get; }
        string Url { get; }

        List<FeatureParent> ChildLocations { get; }

        List<ActivatedFeature> ActivatedFeatures { get; }
        int ChildCount { get; set; }
    }
}