using System;

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
            var farmDummy = GetFarm(Guid.Empty);
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

        public object SPLocationObject { get; private set; }

        public bool RequiresUpdate { get; private set; } = true;
    }
}
