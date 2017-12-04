using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public interface IBaseItem
    {
        string DisplayName { get; }
        Guid Id { get;  }
        Scope Scope { get;  }

        Dictionary<string, string> Details { get; }
    }
}
