using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class Location : BaseItem
    {
        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url)
            :this (id, displayName, parent, scope, url, new List<Guid>())
        {
        }

        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url, IEnumerable<Guid> activatedFeatures)
        {
            Id = id;
            DisplayName = displayName;
            Parent = parent;
            Scope = scope;
            Url = url;
            ActivatedFeatures = (IReadOnlyCollection<Guid>)activatedFeatures;
        }

        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }

        public IReadOnlyCollection<Guid> ActivatedFeatures { get; protected set; }
                
        public Guid Parent { get; protected set; }
        public Scope Scope { get; protected set; }
        public string Url { get; protected set; }

        public static Location GetFarm(Guid farmId, IEnumerable<Guid> activatedFeatures)
        {
            var location = new Location(farmId,
               "Farm",
               Guid.Empty,
               Scope.Farm,
               "Farm");

            return location;
        }

        /// <summary>
        /// adds or removes an activated feature to the list of activated features
        /// </summary>
        /// <param name="location">location, which will be returned with changed feature-list</param>
        /// <param name="featureId">feature Guid</param>
        /// <param name="add">adds if true, removes if false</param>
        /// <returns>location with changed activatedfeatures list</returns>
        public static Location ToggleActivatedFeature(Location location, Guid featureId, bool add)
        {
            var l = new Location(location.Id, location.DisplayName, location.Parent, location.Scope, location.Url);
            var activatedFeatures = new List<Guid>(location.ActivatedFeatures);

            if (add)
            {
                activatedFeatures.Add(featureId);
            }
            else
            {
                activatedFeatures.Remove(featureId);
            }
            
            l.ActivatedFeatures = activatedFeatures.AsReadOnly();
            return l;
        }

        public static Location GetLocation(Guid id, string displayName, Guid parent, Scope scope, string url, IEnumerable<Guid> activatedFeatures)
        {
            var location = new Location(id, displayName, parent, scope, url, activatedFeatures);
            return location;
        }

        public static Location GetLocationUndefined(Guid id, Guid parent, string displayName = "undefined", Scope? scope = Scope.ScopeInvalid, string url = "undefined")
        {
            var location = new Location(id, displayName, parent, scope == null ? Scope.ScopeInvalid : scope.Value, url);
            return location;
        }


    }
}