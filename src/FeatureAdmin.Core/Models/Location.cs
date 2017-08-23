using FeatureAdmin.Core.Models.Contracts;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class Location : ILocation
    {
        public Location(
            Guid id,
            string displayName,
            Scope scope,
            string url,
            ICollection<Guid> activatedFeatures,
            int childCount
            ) : this(id, displayName, scope, url,
                activatedFeatures, null, childCount)
        {}

        public Location(
    Guid id,
    string displayName,
    Scope scope,
    string url,
    ICollection<Guid> activatedFeatures,
    ICollection<Guid> childLocations
    ) : this(id, displayName, scope, url,
        activatedFeatures, childLocations, 0)
        {
            ChildCount = childLocations == null ? 0 : childLocations.Count;
        }

        private Location(
            Guid id, 
            string displayName,
            Scope scope,
            string url,
            ICollection<Guid> activatedFeatures,
            ICollection<Guid> childLocations,
            int childCount
            )
        {
            Id = id;
            DisplayName = displayName;
            Scope = scope;
            Url = url;
            ChildLocations = childLocations ?? new List<Guid>();
            ActivatedFeatures = activatedFeatures ?? new List<Guid>();
            ChildCount = childCount;
        }



        public Guid Id { get; private set; }
        public string DisplayName { get; private set; }
        public Scope Scope { get; private set; }
        public string Url { get; private set; }
        public ICollection<Guid> ChildLocations { get
            {
                return ChildLocations;
            }
            set
            {
                ChildLocations = value;
                ChildCount = ChildLocations == null ? 0 : ChildLocations.Count;
            }
        }

        public ICollection<Guid> ActivatedFeatures { get; private set; }
        public int ChildCount { get; set; }
    }
}