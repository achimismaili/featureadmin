using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models
{
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
        public string DisplayName { get; protected set; }
        public Guid Id { get; protected set; }
        public Scope Scope { get; protected set; }
    

        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }

        public Guid Parent { get;  }

        public string Url { get;  }

        public int ChildCount { get; set; }

    }
}