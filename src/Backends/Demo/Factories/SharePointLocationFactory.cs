using FeatureAdmin.Core.SharePointFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models;

namespace FeatureAdmin.Backends.Demo.Factories
{
    public class SharePointLocationFactory : SharePointLocationFactoryBase
    {
        public override SPObject GetFarm(Location location)
        {
            throw new NotImplementedException();
        }

        public override Location GetFarm(SPObject location)
        {
            throw new NotImplementedException();
        }

        public override SPObject GetSiteCollection(Location location)
        {
            throw new NotImplementedException();
        }

        public override Location GetSiteCollection(SPObject location)
        {
            throw new NotImplementedException();
        }

        public override SPObject GetWeb(Location location)
        {
            throw new NotImplementedException();
        }

        public override Location GetWeb(SPObject location)
        {
            throw new NotImplementedException();
        }

        public override SPObject GetWebApplication(Location location)
        {
            throw new NotImplementedException();
        }

        public override Location GetWebApplication(SPObject location)
        {
            throw new NotImplementedException();
        }
    }
}
