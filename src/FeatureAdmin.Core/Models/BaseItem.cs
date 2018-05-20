using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public abstract class BaseItem 
    {
        public BaseItem(Guid id, string displayName, Scope scope)
        {
            Id = id;
            DisplayName = displayName == null ? string.Empty : displayName;
            Scope = scope;
        }
        [IgnoreDuringEquals]
        public string DisplayName { get; protected set; }
        public Guid Id { get; protected set; }
        public Scope Scope { get; protected set; }
    }
}
