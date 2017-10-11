using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class Location 
    {
        protected Location()
        {

        }
        public string DisplayName { get; private set; }
        public Guid Id { get; private set; }
        public Guid Parent { get; private set; }
        public Scope Scope { get; private set; }
        public string Url { get; private set; }

        public static Location GetDummyFarmForLoadCommand()
        {
            return GetFarm(Guid.Empty);
        }
        public static Location GetFarm(Guid farmId)
        {
            var location = new Location()
            {
                Id = farmId,
                DisplayName = "Farm",
                Url = "Farm",
                Parent = Guid.Empty,
                Scope = Scope.Farm
            };
            return location;
        }

        public static Location GetLocation(Guid id, string displayName, Guid parent, Scope scope, string url)
        {

            var location = new Location()
            {
                Id = id,
                DisplayName = displayName,
                Parent = parent,
                Scope = scope,
                Url = url
            };
            return location;
        }

        public static Location GetLocationUndefined(Guid id, Guid parent, string displayName = "undefined", Scope? scope = Scope.ScopeInvalid, string url = "undefined")
        {
            var location = new Location()
            {
                Id = id,
                DisplayName = displayName,
                Url = url,
                Parent = parent,
                Scope = scope == null ? Scope.ScopeInvalid : scope.Value
            };
            return location;
        }


    }
}