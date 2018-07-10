using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Models;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Services
{
    public interface IDataService
    {

        IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions();

        LoadedDto LoadFarm(bool elevatedPrivileges);

        LoadedDto LoadWebApps(bool elevatedPrivileges);

        /// <summary>
        /// loads site collections and webs of a web application
        /// </summary>
        /// <param name="location">the web app location</param>
        /// <returns>all site collections and webs of a web application including their activated features and some (non farm) feature definitions</returns>
        LoadedDto LoadWebAppChildren(Location location, bool elevatedPrivileges);

        string DeactivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force);
        string ActivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature);
        //int FeatureUninstall(Guid featureId, bool force);

        //int FeatureUpdate(Models.SPObject location, bool add, Guid featureId, bool force);

    }
}
