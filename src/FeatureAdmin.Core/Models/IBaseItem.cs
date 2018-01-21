using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public interface IBaseItem : IDisplayableItem
    {
        IReadOnlyCollection<ActivatedFeature> ActivatedFeatures { get; }
        Guid Id { get;  }
        Scope Scope { get;  }
    }
}
