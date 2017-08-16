using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.SampleData
{
    public static class Features
    {

        public static ICollection<Guid> GetTestFarmFeatures()
        {
            throw new NotImplementedException();
        }

        public static class HealthyWeb
        {
            public static int TotalActivated = 3;
            public static Guid Id = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
            public static string Name = "DummyFeaturesHealthy15_HealthyWeb";
            public static Scope Scope = Scope.Web;
            public static Version Version = new Version("3.0.0.0");
            public static bool Faulty = false;
            public static string Title = "Dummy Features Healthy Web";
            public static int CompatibilityLevel = 15;
            public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);

            public static FeatureDefinition FeatureDefinitionHealthyWeb15
            {
                get
                {
                    return new FeatureDefinition(
                        Id,
                        Title,
                        Name,
                        Scope,
                        CompatibilityLevel,
                        Version,
                        Faulty);
                }
            }

            public static ActivatedFeature HealthyWeb15ActivatedInActivatedRootWeb()
            {
                return new ActivatedFeature(
                       Id,
                       Locations.ActivatedRootWeb.Guid,
                       CompatibilityLevel,
                       Name,
                       TimeActivated,
                       Version,
                       Faulty);
            }
        }
        
        public static class HealthySite
        {
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("bdd4c395-4c92-4bf8-8c61-9d12349bb853");
            public static string Name = "DummyFeaturesHealthy15_HealthySiCo";
            public static Scope Scope = Scope.Site;
            public static Version Version = new Version("3.0.0.0");
            public static bool Faulty = false;
            public const string Title = "Dummy Features Healthy SiCo";
            public static int CompatibilityLevel = 15;
            public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
            public static FeatureDefinition FeatureDefinitionHealthySiCo15
            {
                get
                {
                    return new FeatureDefinition(
                        Id,
                        Title,
                        Name,
                        Scope,
                        CompatibilityLevel,
                        Version,
                        Faulty);
                }
            }

            public static ActivatedFeature HealthySiCo15ActivatedInActivatedSiCo()
            {
                return new ActivatedFeature(
                       Id,
                       Locations.ActivatedSite.Guid,
                       CompatibilityLevel,
                       Name,
                       TimeActivated,
                       Version,
                       Faulty);
            }
        }

        public static class HealthyWebApp
        {
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("cb53cddc-4335-4560-bf29-f1a0c47f8e6a");
            public static string Name = "DummyFeaturesHealthy15_HealthyWebApp";
            public static Scope Scope = Scope.WebApp;
            public static Version Version = new Version("3.0.0.0");
            public static bool Faulty = false;
            public const string Title = "Dummy Features Healthy WebApp";
            public static int CompatibilityLevel = 15;
            public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
            public static FeatureDefinition FeatureDefinitionHealthyWebApp15
            {
                get
                {
                    return new FeatureDefinition(
                        Id,
                        Title,
                        Name,
                        Scope,
                        CompatibilityLevel,
                        Version,
                        Faulty);
                }
            }

            public static ActivatedFeature HealthyWebApp15ActivatedInWebApp()
            {
                return new ActivatedFeature(
                       Id,
                       Locations.WebApp.Guid,
                       CompatibilityLevel,
                       Name,
                       TimeActivated,
                       Version,
                       Faulty);
            }
        }

        public static class HealthyFarm
        {
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("d2cb3620-aacb-459e-842d-dc09aea28828");
            public static string Name = "DummyFeaturesHealthy15_HealthyFarm";
            public static Scope Scope = Scope.Farm;
            public static Version Version = new Version("3.0.0.0");
            public static bool Faulty = false;
            public const string Title = "Dummy Features Healthy Farm";
            public static int CompatibilityLevel = 15;
            public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
            public static FeatureDefinition FeatureDefinitionHealthyFarm15
            {
                get
                {
                    return new FeatureDefinition(
                        Id,
                        Title,
                        Name,
                        Scope,
                        CompatibilityLevel,
                        Version,
                        Faulty);
                }
            }

            public static ActivatedFeature HealthyFarm15ActivatedInFarm()
            {
                return new ActivatedFeature(
                       Id,
                       Locations.TestFarm.Guid,
                       CompatibilityLevel,
                       Name,
                       TimeActivated,
                       Version,
                       Faulty);
            }
        }
        public static class FaultyWeb
        {
            
            public static int TotalActivated = 3;
            public static Guid Id = new Guid("be656c15-c3c9-45e4-af56-ffcf3ef0330a");
            public static string Title = "undefined"; // if not faulty, it would be: "Dummy Features Faulty Web";
            public static Scope Scope = Scope.Web;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = true;
            public static string Name = "Faulty " + Id.ToString(); // if not faulty, it would be:  "DummyFeaturesFaulty_FaultyWeb";
            public static int CompatibilityLevel = 15;
            public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
            public static FeatureDefinition FeatureDefinitionFaultyWeb15
            {
                get
                {
                    return new FeatureDefinition(
                        Id,
                        Title,
                        Name,
                        Scope,
                        CompatibilityLevel,
                        Version,
                        Faulty);
                }
            }

            public static ActivatedFeature FaultyWeb15ActivatedInActivatedRootWeb()
            {
                return new ActivatedFeature(
                       Id,
                       Locations.ActivatedRootWeb.Guid,
                       CompatibilityLevel,
                       Name,
                       TimeActivated,
                       Version,
                       Faulty);
            }
        }

        public static class FaultySite
        {
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("ff832e14-23ea-483e-80f8-5a22cb7297a0");
            public static string Title = "undefined"; // if not faulty, it would be: "Dummy Features Faulty SiCo";
            public static Scope Scope = Scope.Site;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = true;
            public static string Name = "Faulty " + Id.ToString(); // if not faulty, it would be:  "DummyFeaturesFaulty_FaultyWeb";
            public static int CompatibilityLevel = 15;
            public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
            public static FeatureDefinition FeatureDefinitionFaultySite15
            {
                get
                {
                    return new FeatureDefinition(
                        Id,
                        Title,
                        Name,
                        Scope,
                        CompatibilityLevel,
                        Version,
                        Faulty);
                }
            }

            public static ActivatedFeature FaultySite15ActivatedInActivatedSite()
            {
                return new ActivatedFeature(
                       Id,
                       Locations.ActivatedSite.Guid,
                       CompatibilityLevel,
                       Name,
                       TimeActivated,
                       Version,
                       Faulty);
            }
        }

    };

        //public static class FaultyWebApp
        //{
        //    public static int TotalActivated = ?;
        //    public static Guid Id = new Guid("6bf1d2d1-ea35-4bf7-817f-a65549c5ffe9");
        //    public static string Title = "undefined"; // "DummyFeaturesFaulty_FaultyWebApp";
        //    public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.WebApplication;
        //    public static Version Version = new Version("1.0.0.0");
        //    public static bool Faulty = true;
        //     public static string Name = string.Format(Common.Constants.Text.UndefinedActivatedFeature, FaultyWebApp.Id.ToString()); 
        //};

        //public static class FaultyFarm
        //{
        //    public static int TotalActivated = ?;
        //    public static Guid Id = new Guid("c08299d9-65fd-4871-b2e4-8de19315f7e8");
        //    public static string Title = "undefined"; // "DummyFeaturesFaulty_FaultyFarm";
        //    public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.Farm;
        //    public static Version Version = new Version("1.0.0.0");
        //    public static bool Faulty = true;
        //     public static string Name = string.Format(Common.Constants.Text.UndefinedActivatedFeature, FaultyFarm.Id.ToString()); 
        //};
}

