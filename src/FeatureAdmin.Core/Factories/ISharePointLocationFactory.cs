using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.SharePointFactories
{
    public interface ISharePointLocationFactory
    {

        Location GetFarm(SPObject location);

        SPObject GetFarm(Location location);

        Location GetInvalid(SPObject location);

        SPObject GetInvalid(Location location);

        Location GetLocation(SPObject location);

        SPObject GetLocation(Location location);

        Location GetSiteCollection(SPObject location);

        SPObject GetSiteCollection(Location location);

        Location GetWebApplication(SPObject location);

        SPObject GetWebApplication(Location location);

        Location GetWeb(SPObject location);

        SPObject GetWeb(Location location);
    }
}
