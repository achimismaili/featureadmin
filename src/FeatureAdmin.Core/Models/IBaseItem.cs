using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public interface IBaseItem
    {
        IReadOnlyCollection<ActivatedFeature> ActivatedFeatures { get; }
        string DisplayName { get; }
        Guid Id { get;  }
        Scope Scope { get;  }

        List<KeyValuePair<string, string>> GetAsPropertyList();
    }
}
