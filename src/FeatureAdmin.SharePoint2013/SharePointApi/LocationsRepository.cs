using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.Models;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.SharePoint2013.SharePointApi
{
    public class LocationsRepository : ILocationsRepository
    {
        public Location Farm
        {
            get
            {
                var farm = FarmRead.GetFarm();

                return GetLocation(farm);
            }
        }

        public List<Location> GetWebApplicationsAdmin
        {
            get
            {
                var wapps = new List<Location>();

                // get CA web app
                var adminWebApps = FarmRead.GetWebApplicationsAdmin();
                var caIndex = 1;
                foreach (SPWebApplication adminApp in adminWebApps)
                {
                    var index = (adminWebApps.Count == 1) ? string.Empty : " " + caIndex.ToString();
                    var caParent = Location.GetLocation(adminApp, "Central Admin" + index);
                    caParent.ChildCount = adminApp.Sites.Count;
                    wapps.Add(caParent);
                }

                return wapps;
            }
        }

        public List<Location> GetWebApplicationsContent
        {
            get
            {
                var wapps = new List<Location>();

                var contentWebApps = FarmRead.GetWebApplicationsContent();

                foreach (SPWebApplication webApp in contentWebApps)
                {
                        var waAsParent = Location.GetLocation(webApp);
                    waAsParent.ChildCount = webApp.Sites.Count;
                    wapps.Add(waAsParent);
                }

                return wapps;
            }
        }
    }
}
