using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models
{
    [Equals]
    [Serializable]
    public class Location 
    {
        public Location(Guid id, string displayName, Guid parent, Scope scope, string url, int childCount = 0)
        {
            Id = id;
            DisplayName = displayName == null ? string.Empty : displayName;
            Parent = parent;
            Scope = scope;
            Url = url == null ? string.Empty : url;
            ChildCount = childCount;
        }

        [IgnoreDuringEquals]
        public string DisplayName { get; protected set; }
        public Guid Id { get; protected set; }
        public Scope Scope { get; protected set; }

        [IgnoreDuringEquals]
        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }

        [IgnoreDuringEquals]
        public Guid Parent { get;  }

        public string Url { get;  }

        [IgnoreDuringEquals]
        public int ChildCount { get; set; }

        public override string ToString()
        {
            return string.Format(
                "{0}: {1}, URL:'{2}'",
                this.Scope,
                this.DisplayName,
                this.Url);
        }
    }
}