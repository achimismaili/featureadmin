using FeatureAdmin.Core.Models.Contracts;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{ 
    public class Location : ILocation
    {
        public Guid Id { get; private set; }
        public string DisplayName { get; private set; }
        public Scope Scope { get; private set; }
        public string Url { get; private set; }
        public ICollection<Guid> ChildLocations { get; private set; }
        public ICollection<Guid> ActivatedFeatures { get; set; }
        public int ChildCount { get; set; }
    }
}