using FeatureAdmin.Core.Factories;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.SampleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Tests.Models
{
    public class FeatureDefinitionTests
    {
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
                         Name, false,
                         Name, null,
                         Scope,
                         Title,
                         Guid.Empty, "4",
                         Version
                        );
                }
            }
        }




    }
}
