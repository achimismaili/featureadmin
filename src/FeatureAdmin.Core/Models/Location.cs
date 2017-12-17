﻿using FeatureAdmin.Core.Models.Enums;
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

        public override Dictionary<string, string> Details
        {
            get
            {
                var details = new Dictionary<string, string>() {
                    { "Url", this.Url  }
                };

                return details;
            }
        }

        public int ChildCount { get; set; }
    }
}