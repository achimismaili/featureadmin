using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Services
{
    public interface IDataService
    {

        IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions();

        LoadedDto LoadFarm();

        LoadedDto LoadWebApps();

        /// <summary>
        /// loads site collections and webs of a web application
        /// </summary>
        /// <param name="location">the web app location</param>
        /// <returns>all site collections and webs of a web application including their activated features and some (non farm) feature definitions</returns>
        LoadedDto LoadWebAppChildren(Location location, bool elevatedPrivileges);

        string DeactivateFarmFeature(FeatureDefinition feature, Location location, bool force);
        string FarmFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature activatedFeature);
        string DeactivateWebAppFeature(FeatureDefinition feature, Location location, bool force);
        string WebAppFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool force, out ActivatedFeature activatedFeature);
        string DeactivateSiteFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force);
        string SiteFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature);
        string DeactivateWebFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force);
        string WebFeatureAction(FeatureDefinition feature, Location location, FeatureAction action, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature);
 

        //string UninstallFarmFeature(FeatureDefinition feature, bool force);

    }
}
