using FeatureAdmin.Core.Factories;
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
                    return FeatureDefinitionFactory.GetFeatureDefinition(
                        Id, CompatibilityLevel,
                         Title,
                        Name, false,
                        Name, null,
                        Scope,
                        Title,
                        Guid.Empty, "4",
                        Version
                       );
                }
            }

            public static ActivatedFeature HealthyWeb15ActivatedInActivatedRootWeb()
            {
                return ActivatedFeatureFactory.GetActivatedFeature(
                       Id,
                       Locations.ActivatedRootWeb.Guid,
                       FeatureDefinitionHealthyWeb15,
                       Faulty, null, DateTime.Now,
                       Version
                       );
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
                    return FeatureDefinitionFactory.GetFeatureDefinition(
                         Id, CompatibilityLevel,
                         Title,
                         Name,  false,
                         Name, null,
                         Scope,
                         Title,
                         Guid.Empty, "4",
                         Version
                        );
                }
            }

            public static ActivatedFeature HealthySiCo15ActivatedInActivatedSiCo()
            {
                return ActivatedFeatureFactory.GetActivatedFeature(
                       Id,
                       Locations.ActivatedRootWeb.Guid,
                       FeatureDefinitionHealthySiCo15,
                       Faulty, null, DateTime.Now,
                       Version
                       );
            }
        }

        public static class HealthyWebApp
        {
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("cb53cddc-4335-4560-bf29-f1a0c47f8e6a");
            public static string Name = "DummyFeaturesHealthy15_HealthyWebApp";
            public static Scope Scope = Scope.WebApplication;
            public static Version Version = new Version("3.0.0.0");
            public static bool Faulty = false;
            public const string Title = "Dummy Features Healthy WebApp";
            public static int CompatibilityLevel = 15;
            public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
            public static FeatureDefinition FeatureDefinitionHealthyWebApp15
            {
                get
                {
                    return FeatureDefinitionFactory.GetFeatureDefinition(
                         Id, CompatibilityLevel,
                         Title,
                         Name,  false,
                         Name, null,
                         Scope,
                         Title,
                         Guid.Empty, "4",
                         Version
                       );
                }
            }

            public static ActivatedFeature HealthyWebApp15ActivatedInWebApp()
            {
                return ActivatedFeatureFactory.GetActivatedFeature(
                       Id,
                       Locations.ActivatedRootWeb.Guid,
                       FeatureDefinitionHealthyWebApp15,
                       Faulty, null, DateTime.Now,
                       Version
                       );
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
                    return FeatureDefinitionFactory.GetFeatureDefinition(
                         Id, CompatibilityLevel,
                         Title,
                         Name,  false,
                         Name, null,
                         Scope,
                         Title,
                         Guid.Empty, "4",
                         Version
                        );
                }
            }

            public static ActivatedFeature HealthyFarm15ActivatedInFarm()
            {
                return ActivatedFeatureFactory.GetActivatedFeature(
                       Id,
                       Locations.ActivatedRootWeb.Guid,
                       FeatureDefinitionHealthyFarm15,
                       Faulty, null, DateTime.Now,
                       Version
                       );
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
                    return FeatureDefinitionFactory.GetFeatureDefinition(
                         Id, CompatibilityLevel,
                         Title,
                         Name,  false,
                         Name, null,
                         Scope,
                         Title,
                         Guid.Empty, "4",
                         Version
                        );
                }
            }

            public static ActivatedFeature GetFaultyWebFeature(Guid locationId)
            {
                return ActivatedFeatureFactory.GetActivatedFeature(
                        Id,
                        locationId,
                        FeatureDefinitionFaultyWeb15,
                        Faulty, null, DateTime.Now,
                        Version
                        );
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
                    return FeatureDefinitionFactory.GetFeatureDefinition(
                         Id, CompatibilityLevel,
                         Title,
                         Name,  false,
                         Name, null,
                         Scope,
                         Title,
                         Guid.Empty, "4",
                         Version
                          );
                }
            }

            public static ActivatedFeature GetFaultySiteFeature(Guid locationId)
            {
                return ActivatedFeatureFactory.GetActivatedFeature(
                        Id,
                        locationId, // Locations.ActivatedRootWeb.Guid,
                        FeatureDefinitionFaultySite15,
                        Faulty, null, DateTime.Now,
                        Version
                        );
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

