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
        /// <param name="parent">the start location parameter is also returned as updated/refreshed normalized Location out parameter again</param>
        /// <returns>all children site collections and webs</returns>
        /// <remarks>
        /// Web application itself is not included in returned collection, because then it would be forwarded twice, 
        /// once in this collection and once as child collection of farm
        /// </remarks>
        IEnumerable<Location> LoadNonFarmLocationAndChildren(Location location, out Location parent);

        IEnumerable<FeatureDefinition> LoadFarmFeatureDefinitions();

        /// <summary>
        /// Loads farm and all web applications including central administration
        /// </summary>
        /// <param name="farm">parent location farm is returned also as normalized Location out parameter</param>
        /// <returns>list of normalized web applications and farm</returns>
        /// <remarks>not only is the farm returned as out variable, but also included in the returned collection</remarks>
        IEnumerable<Location> LoadFarmAndWebApps(out Location farm);
        
        int FeatureToggle(Location location, FeatureDefinition feature, bool add, bool force);
        //int FeatureUninstall(Guid featureId, bool force);

        //int FeatureUpdate(Models.SPObject location, bool add, Guid featureId, bool force);

    }
}
