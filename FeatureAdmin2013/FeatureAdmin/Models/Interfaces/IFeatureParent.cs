using System;
using Microsoft.SharePoint;

namespace FeatureAdmin.Models.Interfaces
{
    public interface IFeatureParent
    {
        string DisplayName { get; }
        Guid Id { get; }
        SPFeatureScope Scope { get; }
        string Url { get; }
    }
}