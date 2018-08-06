using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Sp2013.Common
{
    internal static class SpLocationHelper
    {

        internal static SPWebService GetFarm()
        {
            return SPWebService.ContentService;
        }

        /// <summary>
        /// get sitecollection based on location id
        /// </summary>
        /// <param name="location">location of sharepoint object to return as SPSite</param>
        /// <returns>undisposed SPSite object</returns>
        /// <remarks>please dispose, if not needed anymore</remarks>
        internal static SPSite GetSite(Location location)
        {
            if (location == null)
            {
                return null;
            }
            return new SPSite(location.Id);
        }

        internal static SPWebCollection GetAllWebs(SPSite site)
        {
            if (site == null)
            {
                return null;
            }
            return site.AllWebs;
        }

        /// <summary>
        /// get web based on location id
        /// </summary>
        /// <param name="location">location of sharepoint object to return as SPWeb</param>
        /// <returns>undisposed SPWeb object</returns>
        /// <remarks>please dispose, if not needed anymore</remarks>
        internal static SPWeb GetWeb(Location location, bool elevatedPrivileges)
        {
            if (location == null)
            {
                return null;
            }

            SPSite spSite = new SPSite(StringHelper.UniqueIdToGuid(location.ParentId));

            if (elevatedPrivileges)
            {
                return SpSiteElevation.SelectAsSystem(spSite, GetWeb, location.Id);
            }
            else
            {
                return spSite.OpenWeb(location.Id);
            }
            
            //TODO: hm, think how to refactor so that it is possible to dispose the spsite object here
        }

        /// <summary>
        /// Helper method for getting web with elevated privileges
        /// </summary>
        /// <param name="site">SPSite</param>
        /// <param name="webId">id of the SPWeb to return</param>
        /// <returns>SPWeb with webId from within the site</returns>
        private static SPWeb GetWeb(SPSite site, Guid webId)
        {
            if (site == null)
            {
                return null;
            }

            return site.OpenWeb(webId);
        }

        internal static IEnumerable<SPWebApplication> GetAllWebApplications()
        {
            var webApps = GetWebApplications(true);

            return webApps.Concat(GetWebApplications(false).AsEnumerable());
        }

        /// <summary>
        /// returns web applications of type content or admin
        /// </summary>
        /// <param name="content">true, if content is requested, false for admin</param>
        /// <returns>content or admin web applications</returns>
        internal static SPWebApplicationCollection GetWebApplications(bool content)
        {
            if (content)
            {
                return SPWebService.ContentService.WebApplications;
            }
            else
            {
                return SPWebService.AdministrationService.WebApplications;
            }
        }


        /// <summary>
        /// returns a web application of type content or admin
        /// </summary>
        /// <param name="content">true, if content is requested, false for admin</param>
        /// <returns>content or admin web application</returns>
        private static SPWebApplication GetWebApplication(Guid id, bool content)
        {
            var webApps = GetWebApplications(content);
            if (webApps == null)
            {
                return null;
            }
            return webApps.FirstOrDefault(wa => wa.Id == id);
        }

        internal static SPWebApplication GetWebApplication(Guid id)
        {
            var wa = GetWebApplication(id, true);

            if (wa != null)
            {
                return wa;
            }

            return GetWebApplication(id, false);
        }


    }
}
