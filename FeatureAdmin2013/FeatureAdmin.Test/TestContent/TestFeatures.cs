using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Test.TestContent
{
    public static class TestFeatures
    {
        public static Guid featureIdHealthyWeb = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static Feature HealthyWebFeature = new Feature(featureIdHealthyWeb);
        public static Guid featureIdHealthySiCo = new Guid("bdd4c395-4c92-4bf8-8c61-9d12349bb853");
        public static Feature HealthySiCoFeature = new Feature(featureIdHealthySiCo);
        public static Guid featureIdHealthyWebApp = new Guid("cb53cddc-4335-4560-bf29-f1a0c47f8e6a");
        public static Feature HealthyWebAppFeature = new Feature(featureIdHealthyWebApp);
        public static Guid featureIdHealthyFarm = new Guid("d2cb3620-aacb-459e-842d-dc09aea28828");
        public static Feature HealthyFarmFeature = new Feature(featureIdHealthyFarm);

        public static Guid featureIdFaultyWeb = new Guid("be656c15-c3c9-45e4-af56-ffcf3ef0330a");
        public static Feature FaultyWebFeature = new Feature(featureIdFaultyWeb);
        public static Guid featureIdFaultySiCo = new Guid("ff832e14-23ea-483e-80f8-5a22cb7297a0");
        public static Feature FaultySiCoFeature = new Feature(featureIdFaultySiCo);
        public static Guid featureIdFaultyWebApp = new Guid("6bf1d2d1-ea35-4bf7-817f-a65549c5ffe9");
        public static Feature FaultyWebAppFeature = new Feature(featureIdFaultyWebApp);
        public static Guid featureIdFaultyFarm = new Guid("c08299d9-65fd-4871-b2e4-8de19315f7e8");
        public static Feature FaultyFarmFeature = new Feature(featureIdFaultyFarm);

        public static List<Feature> AllFaultyFeatures = new List<Feature>()
        {
            FaultyWebFeature, FaultySiCoFeature, FaultyWebAppFeature, FaultyFarmFeature
        };

        public static List<Feature> AllHealthyFeatures
        {
            get
            {
                return new List<Feature>()
                {
                    HealthyWebFeature, HealthySiCoFeature, HealthyWebAppFeature, HealthyFarmFeature
                };
            }
        }

        public static List<Feature> AllFeatures
        {
            get
            {
                var allFeatures = new List<Feature>(AllFaultyFeatures);
                allFeatures.AddRange(AllHealthyFeatures);
                return allFeatures;
            }
        }

        public static List<Feature> AllWebFeatures
        {
            get
            {
                return new List<Feature>()
                {
                    HealthyWebFeature, FaultyWebFeature
                };
            }
        }

        public static List<Feature> AllSiCoFeatures
        {
            get
            {
                return new List<Feature>()
                {
                    HealthySiCoFeature, FaultySiCoFeature
                };
            }
        }

        public static List<Feature> AllWebAppFeatures
        {
            get
            {
                return new List<Feature>()
                {
                    HealthyWebAppFeature, FaultyWebAppFeature
                };
            }
        }

        public static List<Feature> AllFarmFeatures
        {
            get
            {
                return new List<Feature>()
                {
                    HealthyFarmFeature, FaultyFarmFeature
                };
            }
        }
    }
}
