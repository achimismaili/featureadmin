using FeatureAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;

namespace FeatureAdmin.Backends.Error.Services
{
    public class ErrorDataService : IDataService
    {
        public Backend CurrentBackend
        {
            get
            {
               return Backend.ERROR;
            }
        }

        public string DeactivateFarmFeature(FeatureDefinition feature, Location location, bool force)
        {
            throw new NotImplementedException();
        }

        public string DeactivateSiteFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            throw new NotImplementedException();
        }

        public string DeactivateWebAppFeature(FeatureDefinition feature, Location location, bool force)
        {
            throw new NotImplementedException();
        }

        public string DeactivateWebFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force)
        {
            throw new NotImplementedException();
        }

        public string FarmFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature activatedFeature)
        {
            throw new NotImplementedException();
        }

        public LoadedDto LoadFarm()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions()
        {
            throw new NotImplementedException();
        }

        public LoadedDto LoadWebAppChildren(Location location, bool elevatedPrivileges)
        {
            throw new NotImplementedException();
        }

        public LoadedDto LoadWebApps()
        {
            throw new NotImplementedException();
        }

        public string SiteFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature)
        {
            throw new NotImplementedException();
        }

        public string Uninstall(FeatureDefinition definition)
        {
            throw new NotImplementedException();
        }

        public string WebAppFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature activatedFeature)
        {
            throw new NotImplementedException();
        }

        public string WebFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature)
        {
            throw new NotImplementedException();
        }
    }
}
