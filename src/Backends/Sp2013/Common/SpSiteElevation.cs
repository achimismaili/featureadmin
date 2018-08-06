using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Backends.Sp2013.Common
{
    /// <summary>
    /// Elegant SPSite Elevation
    /// </summary>
    /// <remarks>
    /// Based on the great idea of Keith Dahlby
    /// By separating the elevated operation from the logic required to elevate, 
    /// the code is much easier to read and less prone to errors like 
    /// leaking an elevated SPSite, see also 
    /// https://solutionizing.net/2009/01/06/elegant-spsite-elevation/
    /// </remarks>
    public static class SpSiteElevation
    {
        public static SPUserToken GetSystemToken(this SPSite site)
        {
            SPUserToken token = null;
            bool tempCADE = site.CatchAccessDeniedException;

            bool siteIsUnlocked = true;

            try
            {
                //site.CatchAccessDeniedException = false;
                token = site.SystemAccount.UserToken;
            }
            catch (SPException spEx)
            {
                if (site.IsReadLocked)
                {
                    // in this case SiteCollection is locked
                    // not possible to get system account token, continue with a "normal" token
                    siteIsUnlocked = false;

                    token = site.UserToken;
                }
                else
                {
                    throw spEx;
                }
            }
            catch (UnauthorizedAccessException)
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite elevSite = new SPSite(site.ID))
                        token = elevSite.SystemAccount.UserToken;
                });
            }
            finally
            {
                if (siteIsUnlocked)
                {
                    site.CatchAccessDeniedException = tempCADE;
                }
            }
            return token;

        }

        public static void RunAsSystem(this SPSite site, Action<SPSite> action)
        {
            using (SPSite elevSite = new SPSite(site.ID, site.GetSystemToken()))
                action(elevSite);
        }

        public static T SelectAsSystem<T>(this SPSite site, Func<SPSite, T> selector)
        {
            using (SPSite elevSite = new SPSite(site.ID, site.GetSystemToken()))
                return selector(elevSite);
        }

        public static T SelectAsSystem<T>(this SPSite site, Func<SPSite, string, T> selector, string id)
        {
            using (SPSite elevSite = new SPSite(site.ID, site.GetSystemToken()))
                return selector(elevSite, id);
        }

        public static T SelectAsSystem<T>(this SPSite site, Func<SPSite, Guid, T> selector, Guid id)
        {
            using (SPSite elevSite = new SPSite(site.ID, site.GetSystemToken()))
                return selector(elevSite, id);
        }


        public static void RunAsSystem(this SPSite site, Guid webId, Action<SPWeb> action)
        {
            site.RunAsSystem(s => action(s.OpenWeb(webId)));
        }

        //public static void RunAsSystem(this SPSite site, string url, Action<SPWeb> action)
        //{
        //    site.RunAsSystem(s => action(s.OpenWeb(url)));
        //}

        public static void RunAsSystem(this SPWeb web, Action<SPWeb> action)
        {
            web.Site.RunAsSystem(web.ID, action);
        }

        public static T SelectAsSystem<T>(this SPSite site, Guid webId, Func<SPWeb, T> selector)
        {
            return site.SelectAsSystem(s => selector(s.OpenWeb(webId)));
        }

        public static T SelectAsSystem<T>(this SPSite site, Guid webId, Func<SPWeb, string, T> selector, string id)
        {
            return site.SelectAsSystem(s => selector(s.OpenWeb(webId), id));
        }

        //public static T SelectAsSystem<T>(this SPSite site, string url, Func<SPWeb, T> selector)
        //{
        //    return site.SelectAsSystem(s => selector(s.OpenWeb(url)));
        //}

        public static T SelectAsSystem<T>(this SPWeb web, Func<SPWeb, T> selector)
        {
            return web.Site.SelectAsSystem(web.ID, selector);
        }

        public static T SelectAsSystem<T>(this SPWeb web, Func<SPWeb, string, T> selector, string id)
        {
            return web.Site.SelectAsSystem(web.ID, selector, id);
        }
    }
}
