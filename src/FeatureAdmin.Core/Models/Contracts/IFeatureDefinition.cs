using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models.Contracts
{
    /// <summary>
    /// Interface Feature Definition Model
    /// </summary>
    /// <remarks>
    /// see also https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.administration.spfeaturedefinition_properties.aspx
    /// </remarks>
    public interface IFeatureDefinition
    {
        Guid Id { get; }
        int CompatibilityLevel { get; }
        string Description { get; }
        string DisplayName { get; }
        bool Faulty { get; }
        bool Hidden { get; }
        string Name { get; }
        Dictionary<string, string> Properties { get; }
        Scope Scope { get; }
        Guid SolutionId { get; }
        string Title { get; }
        string UIVersion { get; }
        Version Version { get; }
    }
}