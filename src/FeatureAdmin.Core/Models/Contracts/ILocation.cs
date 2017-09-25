using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models.Contracts
{ 
    public interface ILocation
    {
        Guid Id { get; }
        string DisplayName { get; }
        Scope Scope { get; }
        string Url { get; }
        Guid Parent { get; }
    }
}