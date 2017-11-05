using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models
{
    public class Location : BaseItem
    {
        protected List<ActivatedFeature> activatedFeatures { get; set; }

        protected Location()
        {
           this.activatedFeatures = new List<ActivatedFeature>();

        }

        public static Location GetDummyFarmForLoadCommand()
        {
            var farmDummy = GetFarm(Guid.Empty, new List<ActivatedFeature>());
            return farmDummy;
        }

        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url)
            : this()
        {
            Id = id;
            DisplayName = displayName;
            Parent = parent;
            Scope = scope;
            Url = url;
        }

        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url, IEnumerable<ActivatedFeature> activatedFeatures)
            : this(id, displayName, parent, scope, url)
        {
           
            if (activatedFeatures != null && activatedFeatures.Any())
            {
                this.activatedFeatures.AddRange(activatedFeatures);
            }
        }

        public bool CanHaveChildren
        {
            get
            {
                return (Scope != Scope.Web && Scope != Scope.ScopeInvalid);
            }
        }

        public IReadOnlyCollection<ActivatedFeature> ActivatedFeatures { get
            {
                return activatedFeatures.AsReadOnly();
            }
        }
                
        public Guid Parent { get; protected set; }
        public string Url { get; protected set; }

        public static Location GetFarm(Guid farmId, IEnumerable<ActivatedFeature> activatedFeatures)
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
        /// <param name="feature">feature Guid</param>
        /// <param name="add">adds if true, removes if false</param>
        /// <returns>location with changed activatedfeatures list</returns>
        public void ToggleActivatedFeature(ActivatedFeature feature, bool add)
        {
            if (add)
            {
                activatedFeatures.Add(feature);
            }
            else
            {
                activatedFeatures.Remove(feature);
            }
        }

        public static Location GetLocation(Guid id, string displayName, Guid parent, Scope scope, string url, IEnumerable<ActivatedFeature> activatedFeatures)
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