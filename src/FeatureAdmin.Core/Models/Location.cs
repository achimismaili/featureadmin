using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class Location
    {
        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url)
        {
            Id = id;
            DisplayName = displayName;
            Parent = parent;
            Scope = scope;
            Url = url;
        }

        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }
        public string DisplayName { get; protected set; }
        public Guid Id { get; protected set; }
        public Guid Parent { get; protected set; }
        public Scope Scope { get; protected set; }
        public string Url { get; protected set; }

        public static Location GetFarm(Guid farmId)
        {
            var location = new Location(farmId,
               "Farm",
               Guid.Empty,
               Scope.Farm,
               "Farm");

            return location;
        }

        public static Location GetLocation(Guid id, string displayName, Guid parent, Scope scope, string url)
        {
            var location = new Location(id, displayName, parent, scope, url);
            return location;
        }

        public static Location GetLocationUndefined(Guid id, Guid parent, string displayName = "undefined", Scope? scope = Scope.ScopeInvalid, string url = "undefined")
        {
            var location = new Location(id, displayName, parent, scope == null ? Scope.ScopeInvalid : scope.Value, url);
            return location;
        }


    }
}