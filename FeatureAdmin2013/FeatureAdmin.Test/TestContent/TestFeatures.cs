using FeatureAdmin.Models;
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
        public static class HealthyWeb {
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
        };

        public static class FaultyWeb
        {
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


        public static List<Guid> AllFaultyFeatures = new List<Guid>()
        {
            FaultyWeb.Id, FaultySite.Id // , FaultyWebApp.Id, FaultyFarm.Id
        };

        public static List<Guid> AllHealthyFeatures = new List<Guid>()
                {
                    HealthyWeb.Id, HealthySite.Id, HealthyWebApp.Id, HealthyFarm.Id
                };
         
        public static List<Guid> AllFeatures
        {
            get
            {
                var allFeatures = new List<Guid>(AllFaultyFeatures);
                allFeatures.AddRange(AllHealthyFeatures);
                return allFeatures;
            }
        }

        public static List<Guid> AllWebFeatures= new List<Guid>()
                {
                    HealthyWeb.Id, FaultyWeb.Id
                };

        public static List<Guid> AllSiCoFeatures = new List<Guid>()
                {
                    HealthySite.Id, FaultySite.Id
                };
    }
}
