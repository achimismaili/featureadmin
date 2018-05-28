using FeatureAdmin.Core.Messages.Completed;
using FeatureAdmin.Core.Models;
using System.Collections.Generic;

namespace FeatureAdmin.Core.Services
{
    public interface IDataService
    {

        /// <summary>
        /// Loads a web application and all its children site collections and their children webs 
        /// </summary>
        /// <param name="location">the start location, what to load. This is in version 3.0 always a web application, which is already up to date</param>
        /// <returns>all children site collections and webs within a locations loaded message</returns>
        /// <remarks>
        /// Web application itself is not included in returned collection, because then it would be forwarded twice, 
        /// once in this collection and once as child collection of farm
        /// </remarks>
        LocationsLoaded LoadNonFarmLocationAndChildren(Location location);

        IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions();

        /// <summary>
        /// Loads farm and all web applications including central administration
        /// </summary>
        /// <returns>list of normalized web applications and farm within a locations loaded message</returns>
        /// <remarks>not only is the farm returned as out variable, but also included in the returned collection</remarks>
        LocationsLoaded LoadFarmAndWebApps();
        
        string DeactivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force);
        string ActivateFeature(FeatureDefinition feature, Location location, bool elevatedPrivileges, bool force, out ActivatedFeature activatedFeature);
        //int FeatureUninstall(Guid featureId, bool force);

        //int FeatureUpdate(Models.SPObject location, bool add, Guid featureId, bool force);

    }
}
