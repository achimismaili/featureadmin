using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.Models;
using Microsoft.SharePoint.Administration;

namespace FA.SharePoint
{
    public class LocationsRepository : ILocationsRepository
    {
        public FeatureParent Farm
        {
            get
            {
                var farm = FarmRead.GetFarm();

                return FeatureParent.GetFeatureParent(farm);
            }
        }

        public List<FeatureParent> GetWebApplicationsAdmin
        {
            get
            {
                var wapps = new List<FeatureParent>();

                // get CA web app
                var adminWebApps = FarmRead.GetWebApplicationsAdmin();
                var caIndex = 1;
                foreach (SPWebApplication adminApp in adminWebApps)
                {
                    var index = (adminWebApps.Count == 1) ? string.Empty : " " + caIndex.ToString();
                    var caParent = FeatureParent.GetFeatureParent(adminApp, "Central Admin" + index);
                    caParent.ChildCount = adminApp.Sites.Count;
                    wapps.Add(caParent);
                }

                return wapps;
            }
        }

        public List<FeatureParent> GetWebApplicationsContent
        {
            get
            {
                var wapps = new List<FeatureParent>();

                var contentWebApps = FarmRead.GetWebApplicationsContent();

                foreach (SPWebApplication webApp in contentWebApps)
                {
                        var waAsParent = FeatureParent.GetFeatureParent(webApp);
                    waAsParent.ChildCount = webApp.Sites.Count;
                    wapps.Add(waAsParent);
                }

                return wapps;
            }
        }
    }
}
