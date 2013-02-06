using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    class ActivationFinder
    {
        /// <summary>
        /// Delegate to report when feature found
        /// </summary>
        /// <param name="location"></param>
        public delegate void FeatureFoundHandler(string location);
        public event FeatureFoundHandler FoundListeners;
        protected void OnFound(string msg) { if (FoundListeners != null) FoundListeners(msg); }

        /// <summary>
        /// Delegate to report exception
        /// </summary>
        /// <param name="msg"></param>
        public delegate void ExceptionHandler(Exception exc, string msg);
        public event ExceptionHandler ExceptionListeners;
        protected void OnException(Exception exc, string msg) { if (ExceptionListeners != null) ExceptionListeners(exc, msg); }

        /// <summary>
        /// Find and report first place where feature activated, or return false if none found
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public bool FindFeatureActivations(Feature feature)
        {
            string msgString = string.Empty;

            //first, Look in Farm
            if ((SPWebService.ContentService.Features[feature.Id] is SPFeature))
            {
                msgString = "Farm Feature is activated in the Farm on farm level!";
                OnFound(msgString);
                return true;
            }

            // iterate through web apps
            SPWebApplicationCollection webApplicationCollection = SPWebService.ContentService.WebApplications;

            foreach (SPWebApplication webApp in webApplicationCollection)
            {
                // check web apps
                if (webApp.Features[feature.Id] is SPFeature)
                {
                    msgString = "Web App scoped Feature is activated in WebApp '" + webApp.Name.ToString() + "'!";
                    OnFound(msgString);
                    return true;
                }

                try
                {
                    foreach (SPSite site in webApp.Sites)
                    {
                        using (site)
                        {
                            // check sites
                            if (site.Features[feature.Id] is SPFeature)
                            {
                                msgString = "Site Feature is activated in SiteCollection '" + site.Url.ToString() + "'!";
                                OnFound(msgString);
                                return true;
                            }
                            // check subwebs
                            foreach (SPWeb web in site.AllWebs)
                            {
                                using (web)
                                {
                                    // check webs
                                    if (web.Features[feature.Id] is SPFeature)
                                    {
                                        msgString = "Web scoped Feature is activated in Site '" + web.Url.ToString() + "'!";
                                        OnFound(msgString);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    msgString = "Exception attempting to enumerate sites of WebApp: " + webApp.Name;
                    OnException(exc, msgString);
                }
            }
            return false;
        }
    }
}
