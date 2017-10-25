using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models
{
    public class Location : BaseItem
    {
        protected List<Guid> features { get; set; }

        protected Location()
        {
           this.features = new List<Guid>();

        }

        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url)
            :this (id, displayName, parent, scope, url, new List<Guid>())
        {
        }

        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url, IEnumerable<Guid> activatedFeatures)
           : this()
        {
            Id = id;
            DisplayName = displayName;
            Parent = parent;
            Scope = scope;
            Url = url;
            if (activatedFeatures != null && activatedFeatures.Any())
            {
                this.features.AddRange(activatedFeatures);
            }
        }

        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }

        public IReadOnlyCollection<Guid> ActivatedFeatures { get
            {
                return null; // activatedFeatures.AsReadOnly();
            }
        }
                
        public Guid Parent { get; protected set; }
        public string Url { get; protected set; }

        public static Location GetFarm(Guid farmId, IEnumerable<Guid> activatedFeatures)
        {
            var location = new Location(farmId,
               "Farm",
               Guid.Empty,
               Scope.Farm,
               "Farm",
               activatedFeatures);

            return location;
        }

        /// <summary>
        /// adds or removes an activated feature 
        /// </summary>
        /// <param name="featureId">feature Guid</param>
        /// <param name="add">adds if true, removes if false</param>
        /// <returns>location with changed activatedfeatures list</returns>
        public void ToggleActivatedFeature(Guid featureId, bool add)
        {
            if (add)
            {
                features.Add(featureId);
            }
            else
            {
                features.Remove(featureId);
            }
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