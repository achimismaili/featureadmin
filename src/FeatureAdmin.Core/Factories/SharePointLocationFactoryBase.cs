using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.SharePointFactories
{
    public abstract class SharePointLocationFactoryBase: ISharePointLocationFactory
    {

        public abstract Location GetFarm(SPObject location);

        public abstract SPObject GetFarm(Location location);

        public Location GetInvalid(SPObject location) {
            //TODO try to get more info from invalid object, e.g. url or guid
            return Location.GetLocationUndefined(Guid.Empty, Guid.Empty);
        }

        public SPObject GetInvalid(Location location) {
            // TODO maybe add debug log ...
            return SPObject.GetSPObject(null,SPObjectType.Location, Scope.ScopeInvalid);
        }

        public Location GetLocation(SPObject location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("Location or Scope is null");
            }

            switch (location.Scope)
            {
                case Scope.Web:
                    return GetWeb(location);
                case Scope.Site:
                    return GetSiteCollection(location);
                case Scope.WebApplication:
                    return GetWeb(location);
                case Scope.Farm:
                    return GetWeb(location);
                case Scope.ScopeInvalid:
                default:
                    return GetInvalid(location);
            }
        }

        public SPObject GetLocation(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("Location or Scope is null");
            }
            switch (location.Scope)
            {
                case Scope.Web:
                    return GetWeb(location);
                case Scope.Site:
                    return GetSiteCollection(location);
                case Scope.WebApplication:
                    return GetWeb(location);
                case Scope.Farm:
                    return GetWeb(location);
                case Scope.ScopeInvalid:
                default:
                    return GetInvalid(location);
            }
        }

        public abstract Location GetSiteCollection(SPObject location);

        public abstract SPObject GetSiteCollection(Location location);

        public abstract Location GetWebApplication(SPObject location);

        public abstract SPObject GetWebApplication(Location location);

        public abstract Location GetWeb(SPObject location);

        public abstract SPObject GetWeb(Location location);
    }
}
