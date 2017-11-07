using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models
{
    public class Location : BaseItem<ActivatedFeature>
    {
        

        protected Location():base()
        {
        }

        public IReadOnlyCollection<ActivatedFeature> ActivatedFeatures
        {
            get
            {
                return activatedFeatures.AsReadOnly();
            }
        }

        protected Location(Guid id, string displayName, Guid parent, Scope scope, string url)
            : this()
        {
            Id = id;
            DisplayName = displayName == null ? string.Empty : displayName;
            Parent = parent;
            Scope = scope;
            Url = url == null ? string.Empty : url;
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

        public Guid Parent { get;  }

        public string Url { get;  }

        public static Location GetDummyFarmForLoadCommand()
        {
            var farmDummy = GetFarm(Guid.Empty, new List<ActivatedFeature>());
            return farmDummy;
        }
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