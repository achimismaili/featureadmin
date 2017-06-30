using FeatureAdmin.Models;
using FeatureAdmin.Models.Interfaces;
using FeatureAdmin.Test.TestContent.MockModels;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Test.TestContent
{
    public static class TestFeatures
    {
        public static class HealthyWeb
        {
            public static MockFeatureDefinition GetMockFeatureDefinition()
            {
                return new MockFeatureDefinition()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    GetTitle = Title
                };
            }
            public static MockActivatedFeature GetMockActivatedFeatureInSiCoActivatedRootWeb()
            {

                var SiCoActivatedParent = SharePointContainers.SiCoActivated.RootWeb.GetMockFeatureParent();
                return GetMockActivatedFeature(SiCoActivatedParent);
            }
            public static MockActivatedFeature GetMockActivatedFeature(IFeatureParent parent)
            {
                return new MockActivatedFeature()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    Parent = parent,
                    Version = Version
                };
            }
            public static int TotalActivated = 3;
            public static Guid Id = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
            public static string Name = "DummyFeaturesHealthy_HealthyWeb";
            public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.Web;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = false;
            public static string Title = "Dummy Features Healthy Web";
        };

        public static class HealthySite
        {
            public static MockFeatureDefinition GetMockFeatureDefinition()
            {
                return new MockFeatureDefinition()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    GetTitle = Title
                };
            }
            public static MockActivatedFeature GetMockActivatedFeatureInSiCoActivated()
            {

                var SiCoActivatedParent = SharePointContainers.SiCoActivated.GetMockFeatureParent();
                return GetMockActivatedFeature(SiCoActivatedParent);
            }
            public static MockActivatedFeature GetMockActivatedFeature(IFeatureParent parent)
            {
                return new MockActivatedFeature()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    Parent = SharePointContainers.SiCoActivated.GetMockFeatureParent(),
                    Version = Version
                };
            }
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("bdd4c395-4c92-4bf8-8c61-9d12349bb853");
            public static string Name = "DummyFeaturesHealthy_HealthySiCo";
            public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.Site;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = false;
            public const string Title = "Dummy Features Healthy SiCo";
        };

        public static class HealthyWebApp
        {
            public static MockFeatureDefinition GetMockFeatureDefinition()
            {
                return new MockFeatureDefinition()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    GetTitle = Title
                };
            }
            public static MockActivatedFeature GetMockActivatedFeature()
            {
                return new MockActivatedFeature()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    Parent = SharePointContainers.WebApplication.GetMockFeatureParent(),
                    Version = Version
                };
            }
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("cb53cddc-4335-4560-bf29-f1a0c47f8e6a");
            public static string Name = "DummyFeaturesHealthy_HealthyWebApp";
            public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.WebApplication;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = false;
            public const string Title = "Dummy Features Healthy WebApp";
        };

        public static class HealthyFarm
        {
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("d2cb3620-aacb-459e-842d-dc09aea28828");
            public static string Name = "DummyFeaturesHealthy_HealthyFarm";
            public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.Farm;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = false;
            public const string Title = "Dummy Features Healthy Farm";
            public static MockFeatureDefinition GetMockFeatureDefinition()
            {
                return new MockFeatureDefinition()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    GetTitle = Title
                };
            }
            public static MockActivatedFeature GetMockActivatedFeature()
            {
                return new MockActivatedFeature()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    Parent = SharePointContainers.Farm.GetMockFeatureParent(),
                    Version = Version
                };
            }
        };

        public static class FaultyWeb
        {
            public static MockFeatureDefinition GetMockFeatureDefinition()
            {
                return new MockFeatureDefinition()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    GetTitle = Title
                };
            }
            public static MockActivatedFeature GetMockActivatedFeatureInSiCoActivatedRootWeb()
            {

                var SiCoActivatedParent = SharePointContainers.SiCoActivated.RootWeb.GetMockFeatureParent();
                return GetMockActivatedFeature(SiCoActivatedParent);
            }
            public static MockActivatedFeature GetMockActivatedFeature(IFeatureParent parent)
            {
                return new MockActivatedFeature()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    Parent = parent,
                    Version = Version
                };
            }
            public static int TotalActivated = 3;
            public static Guid Id = new Guid("be656c15-c3c9-45e4-af56-ffcf3ef0330a");
            public static string Title = "undefined"; // if not faulty, it would be: "Dummy Features Faulty Web";
            public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.Web;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = true;
            public static string Name = string.Format(Common.Constants.Text.UndefinedActivatedFeature, FaultyWeb.Id.ToString()); // if not faulty, it would be:  "DummyFeaturesFaulty_FaultyWeb";
        };

        public static class FaultySite
        {
            public static MockFeatureDefinition GetMockFeatureDefinition()
            {
                return new MockFeatureDefinition()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    GetTitle = Title
                };
            }
            public static MockActivatedFeature GetMockActivatedFeatureInSiCoActivated()
            {

                var SiCoActivatedParent = SharePointContainers.SiCoActivated.GetMockFeatureParent();
                return GetMockActivatedFeature(SiCoActivatedParent);
            }
            public static MockActivatedFeature GetMockActivatedFeature(IFeatureParent parent)
            {
                return new MockActivatedFeature()
                {
                    Id = Id,
                    Name = Name,
                    Scope = Scope,
                    DefinitionVersion = Version,
                    Faulty = Faulty,
                    Parent = SharePointContainers.SiCoActivated.GetMockFeatureParent(),
                    Version = Version
                };
            }
            public static int TotalActivated = 1;
            public static Guid Id = new Guid("ff832e14-23ea-483e-80f8-5a22cb7297a0");
            public static string Title = "undefined"; // if not faulty, it would be: "Dummy Features Faulty SiCo";
            public static SPFeatureScope Scope = Microsoft.SharePoint.SPFeatureScope.Site;
            public static Version Version = new Version("1.0.0.0");
            public static bool Faulty = true;
            public static string Name = string.Format(Common.Constants.Text.UndefinedActivatedFeature, FaultySite.Id.ToString()); // if not faulty, it would be: "DummyFeaturesFaulty_FaultySiCo";
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


        public static List<MockActivatedFeature> AllFaultyActivatedFeatures = new List<MockActivatedFeature>()
        {
            FaultyWeb.GetMockActivatedFeatureInSiCoActivatedRootWeb(),
            FaultyWeb.GetMockActivatedFeature(TestContent.SharePointContainers.SiCoActivated.SubWebActivated.GetMockFeatureParent()),
            FaultyWeb.GetMockActivatedFeature(TestContent.SharePointContainers.SiCoInActive.SubWebActivated.GetMockFeatureParent()),
            FaultySite.GetMockActivatedFeatureInSiCoActivated() 
            // , FaultyWebApp.Id, FaultyFarm.Id
        };

        public static List<MockActivatedFeature> AllHealthyActivatedFeatures = new List<MockActivatedFeature>()
                {
            HealthyWeb.GetMockActivatedFeatureInSiCoActivatedRootWeb(),
            HealthyWeb.GetMockActivatedFeature(SharePointContainers.SiCoActivated.SubWebActivated.GetMockFeatureParent()),
            HealthyWeb.GetMockActivatedFeature(SharePointContainers.SiCoInActive.SubWebActivated.GetMockFeatureParent()),
            HealthySite.GetMockActivatedFeatureInSiCoActivated(),
            HealthyWebApp.GetMockActivatedFeature(),
            HealthyFarm.GetMockActivatedFeature()
                            };

        public static List<MockActivatedFeature> AllActiveFeatures
        {
            get
            {
                var allFeatures = new List<MockActivatedFeature>(AllFaultyActivatedFeatures);
                allFeatures.AddRange(AllHealthyActivatedFeatures);
                return allFeatures;
            }
        }

        public static List<MockFeatureDefinition> AllFaultyFeatureDefinitions = new List<MockFeatureDefinition>()
        {
            FaultyWeb.GetMockFeatureDefinition(),
            FaultySite.GetMockFeatureDefinition() 
            // , FaultyWebApp.Id, FaultyFarm.Id
        };

        public static List<MockFeatureDefinition> AllHealthyFeatureDefinitions = new List<MockFeatureDefinition>()
        {
          HealthyWeb.GetMockFeatureDefinition(),
          HealthySite.GetMockFeatureDefinition(),
          HealthyWebApp.GetMockFeatureDefinition(),
          HealthyFarm.GetMockFeatureDefinition()
        };

        public static List<MockFeatureDefinition> AllFeatureDefinitions
        {
            get
            {
                var allFeatures = new List<MockFeatureDefinition>(AllFaultyFeatureDefinitions);
                allFeatures.AddRange(AllHealthyFeatureDefinitions);
                return allFeatures;
            }
        }


        public static List<MockFeatureDefinition> AllWebFeatureDefinitions = new List<MockFeatureDefinition>()
                {

            HealthyWeb.GetMockFeatureDefinition(), FaultyWeb.GetMockFeatureDefinition()
                };

        public static List<MockFeatureDefinition> AllSiCoFeatureDefinitions = new List<MockFeatureDefinition>()
                {
                    HealthySite.GetMockFeatureDefinition(), FaultySite.GetMockFeatureDefinition()
                };
    }
}
