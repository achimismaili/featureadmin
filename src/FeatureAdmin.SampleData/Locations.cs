using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SampleData
{
    public static class Locations
    {
        public static class TestFarm
        {
            public static Guid Guid = new Guid("2e977989-405f-4437-a589-1d915956b62f");
            public static string DisplayName = "Test Farm";
            public static Scope Scope = Scope.Farm;
            public static string Url = "Farm";

            public static Location Location
            {
                get
                {
                    return new Location(
                        Guid, DisplayName, Scope, Url,
                        Features.GetTestFarmFeatures(), 5 );
                }
            }
        }

        public static class WebApp
        {
            public static Guid Guid = new Guid("0e1f69d3-5e32-48a8-be5d-20ca0f788a62");
            public static string DisplayName = "Test Web Application";
            public static Scope Scope = Scope.WebApp;
            public static string Url = "https://testfarm.com/";
            public static Location Location
            {
                get
                {
                    var af = new List<Guid>();
                    af.Add(Features.HealthyWebApp.Id);
                    return new Location(
                        Guid, DisplayName, Scope, Url, af, 0);
                }
            }

        }

        public static class ActivatedSite
        {
            public static Guid Guid = new Guid("136afd8b-bbec-43cf-a412-e19210819727");
            public static string DisplayName = "Healthy Site";
            public static Scope Scope = Scope.Site;
            public static string Url = "https://testfarm.com/sites/activated/";
            public static Location Location
            {
                get
                {
                    var af = new List<Guid>();
                    af.Add(Features.HealthySite.Id);
                    return new Location(
                        Guid, DisplayName, Scope, Url, af, 0);
                }
            }

        }

            public static class ActivatedRootWeb
        {
            public static Guid Guid = new Guid("3cfc9c79-1761-4aa5-862a-0e03944ef099");
            public static string DisplayName = "Healthy RootWeb";
            public static Scope Scope = Scope.Web;
            public static string Url = "https://testfarm.com/sites/activated/";
            public static Location Location
            {
                get
                {
                    var af = new List<Guid>();
                    af.Add(Features.HealthyWeb.Id);
                    return new Location(
                        Guid, DisplayName, Scope, Url,af, 0);
                }
            }
        }
    }
}
