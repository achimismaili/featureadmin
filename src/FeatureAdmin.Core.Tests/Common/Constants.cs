using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Tests.Common
{
    public static class Constants
    {
        public static class GenericValues { 
        public static Guid Id = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static Guid SolutionId = new Guid("5a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static string Name = "Test Name";
        public static string DisplayName = "Test Display Name";
        public static string Description = "Test description";
        public static bool Hidden = false;
        public static FeatureDefinitionScope DefinitioninstallationScope = FeatureDefinitionScope.Farm;
        public static Scope Scope = Scope.Web;
        public static Version Version = new Version("3.0.0.0");
        public static bool Faulty = false;
        public static string Title = "Dummy Features Healthy Web";
        public static int CompatibilityLevel = 15;
        public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
        public static Dictionary<string, string> Properties = new Dictionary<string, string>() { { "asdf1", "asdfasdf" } };
        public static string UiVersion = "4";
            public static string Url = "https://www.featureadmin.com";

            public static Guid IdDifferent = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e78");
        public static Guid SolutionIdDifferent = new Guid("4a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static string NameDifferent = "DummyFeaturesHealthy15_HealthyWeb1";
        public static string DisplayNameDifferent = "Different Test Display Name";
        public static string DescriptionDifferent = "Different Test description";
        public static bool HiddenDifferent = !Hidden;
        public static string DefinitioninstallationScopeDifferent = "Different";
        public static Scope ScopeDifferent = Scope.Site;
        public static Version VersionDifferent = new Version("3.0.0.1");
        public static bool FaultyDifferent = !Faulty;
        public static string TitleDifferent = "Dummy Features Healthy Web1";
        public static int CompatibilityLevelDifferent = 14;
        public static DateTime TimeActivatedDifferent = new DateTime(1975, 6, 27, 20, 15, 33);
        public static Dictionary<string, string> PropertiesDifferent = new Dictionary<string, string>() { { "asdf2", "asdfasdf" } };
        public static string UiVersionDifferent = "3";
            public static string UrlDifferent = "https://www.featureadmin.com/sites/test";
        }
    }
}
