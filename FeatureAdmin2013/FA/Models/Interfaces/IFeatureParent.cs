using System;
using Microsoft.SharePoint;

namespace FA.Models.Interfaces
{
    public interface IFeatureParent
    {
        string DisplayName { get; }
        Guid Id { get; }
        SPFeatureScope Scope { get; }
        string Url { get; }
    }
}