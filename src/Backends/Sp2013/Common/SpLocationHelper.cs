using FeatureAdmin.Core.Models;
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

        internal static SPWebService GetFarm(bool elevatedPrivileges)
        {
            return SPWebService.ContentService;
        }

        /// <summary>
        /// get sitecollection based on location id
        /// </summary>
        /// <param name="location">location of sharepoint object to return as SPSite</param>
        /// <returns>undisposed SPSite object</returns>
        /// <remarks>please dispose, if not needed anymore</remarks>
        internal static SPSite GetSite(Location location, bool elevatedPrivileges)
        {
            if (location == null)
            {
                return null;
            }
            return new SPSite(location.Id);
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

            SPSite oSPsite = new SPSite(location.Parent);
            return oSPsite.OpenWeb(location.Id);
        }

        internal static IEnumerable<SPWebApplication> GetAllWebApplications(bool elevatedPrivileges)
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
        internal static SPWebApplication GetWebApplication(Guid id, bool content, bool elevatedPrivileges)
        {
            var webApps = GetWebApplications(content);
            if (webApps == null)
            {
                return null;
            }
            return webApps.FirstOrDefault(wa => wa.Id == id);
        }

        internal static SPWebApplication GetWebApplication(Guid id, bool elevatedPrivileges)
        {
            var wa = GetWebApplication(id, true, elevatedPrivileges);

            if (wa != null)
            {
                return wa;
            }

            return GetWebApplication(id, false, elevatedPrivileges);
        }


    }
}
