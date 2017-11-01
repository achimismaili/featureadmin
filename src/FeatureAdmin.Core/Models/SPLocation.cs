using System;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Models
{
    public class SPLocation : Location
    {
        protected SPLocation(Location location,
            object spLocationObject,
            bool requiresUpdate):base(location.Id, location.DisplayName, location.Parent, location.Scope, location.Url)
        {
            SPLocationObject = spLocationObject;
            RequiresUpdate = requiresUpdate;
        }

        public static SPLocation GetDummyFarmForLoadCommand()
        {
            var farmDummy = GetFarm(Guid.Empty, new List<ActivatedFeature>());
            return ToSPLocation(farmDummy);
        }

        public static SPLocation ToSPLocation(Location location)
        {
            return GetSPLocation(location, null, true);
        }

        public static SPLocation GetSPLocation(
            Location location,
            object spLocationObject,
            bool requiresUpdate
            )
        {
            SPLocation spLocation = new SPLocation(location, spLocationObject, requiresUpdate);
            return spLocation;
        }

        public Location ToLocation()
        {

            var location = Location.GetLocation(Id, DisplayName, Parent, Scope, Url, activatedFeatures);

            return location;
        }


        public object SPLocationObject { get; private set; }

        public bool RequiresUpdate { get; private set; } = true;
    }
}
