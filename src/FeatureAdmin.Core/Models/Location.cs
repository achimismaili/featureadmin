using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Core.Models
{
    public class Location : BaseItem
    {
        

        protected Location():base()
        {
        }

        public Location(Guid id, string displayName, Guid parent, Scope scope, string url, int childCount)
            : this()
        {
            Id = id;
            DisplayName = displayName == null ? string.Empty : displayName;
            Parent = parent;
            Scope = scope;
            Url = url == null ? string.Empty : url;
            ChildCount = childCount;
        }

        public Location(Guid id, string displayName, Guid parent, Scope scope, string url, IEnumerable<ActivatedFeature> activatedFeatures, int childCount)
            : this(id, displayName, parent, scope, url, childCount)
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

        public int ChildCount { get; set; }

        public override List<KeyValuePair<string, string>> GetAsPropertyList()
        {
            var propList = new List<KeyValuePair<string, string>>(); 

            propList.Add(new KeyValuePair<string, string>(nameof(Parent), string.Format("Location Id: {0}", Parent.ToString())));
            propList.Add(new KeyValuePair<string, string>(nameof(Url), Url));
            propList.Add(new KeyValuePair<string, string>(nameof(ChildCount), ChildCount.ToString()));

            propList.AddRange(GetAsPropertyList(false));

            return propList;
        }
    }
}