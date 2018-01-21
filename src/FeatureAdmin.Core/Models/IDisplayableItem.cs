using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public interface IDisplayableItem
    {
        string DisplayName { get; }

        List<KeyValuePair<string, string>> GetAsPropertyList();
    }
}
