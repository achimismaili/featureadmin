using FeatureAdmin.Core.Factories;
using FeatureAdmin.Core.Models;
using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.SampleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeatureAdmin.Core.Tests.Models
{
    public class FeatureDefinitionEqualTests
    {
        public static Guid Id = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static Guid SolutionId = new Guid("5a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static string Name = "DummyFeaturesHealthy15_HealthyWeb";
        public static string DisplayName = "Test Display Name";
        public static string Description = "Test description";
        public static bool Hidden = false;
        public static string DefinitioninstallationScope = "Farm";
        public static Scope Scope = Scope.Web;
        public static Version Version = new Version("3.0.0.0");
        public static bool Faulty = false;
        public static string Title = "Dummy Features Healthy Web";
        public static int CompatibilityLevel = 15;
        public static DateTime TimeActivated = new DateTime(1974, 6, 27, 20, 15, 33);
        public static Dictionary<string, string> Properties = new Dictionary<string, string>() { { "asdf1", "asdfasdf" } };
        public static string UiVersion = "4";

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


        [Fact]
        // Following needs to be equal:
        // FeatureId, Compatibility level, Name, Scope, DefinitioninstallationScope
        public void DefinitionsWithSameFeatureIdCompLevelNameAndScopeAreEqual()
        {



            // Arrange
            var referenceFeature = FeatureDefinitionFactory.GetFeatureDefinition(
                            Id, CompatibilityLevel,
                             Description,
                            DisplayName, Hidden,
                            Name, Properties,
                            Scope,
                            Title,
                            SolutionId, UiVersion,
                            Version, DefinitioninstallationScope
                           );

            ActivatedFeature ActiveFeature1 = ActivatedFeatureFactory.GetActivatedFeature(
                  Id,
                  Locations.ActivatedRootWeb.Guid,
                  referenceFeature,
                  Faulty, null, DateTime.Now,
                  Version
                  );

            referenceFeature.ToggleActivatedFeature(ActiveFeature1, true);

            // Act
            var equalFeature = FeatureDefinitionFactory.GetFeatureDefinition(
                    Id, CompatibilityLevel,
                     DescriptionDifferent,
                    DisplayNameDifferent, HiddenDifferent,
                    Name, PropertiesDifferent,
                    Scope,
                    TitleDifferent,
                    SolutionIdDifferent, UiVersionDifferent,
                    VersionDifferent, DefinitioninstallationScope
                   );

            // Assert
            
            Assert.Equal(referenceFeature, equalFeature);

            // Act
            var equalFeatureEmpty = FeatureDefinitionFactory.GetFeatureDefinition(
                    Id, CompatibilityLevel,
                     null,
                    null, HiddenDifferent,
                    Name, null,
                    Scope,
                    null,
                    Guid.Empty, null,
                    null, DefinitioninstallationScope
                   );

            // Assert

            Assert.Equal(referenceFeature, equalFeatureEmpty);


        }

        [Fact]
        // Following needs to be equal:
        // FeatureId, Compatibility level, Name, Scope, DefinitioninstallationScope
        public void DefinitionsWithDifferentFeatureIdCompLevelNameOrScopeAreNotEqual()
        {



            // Arrange
            var referenceFeature = FeatureDefinitionFactory.GetFeatureDefinition(
                            Id, CompatibilityLevel,
                             Description,
                            DisplayName, Hidden,
                            Name, Properties,
                            Scope,
                            Title,
                            SolutionId, UiVersion,
                            Version, DefinitioninstallationScope
                           );

            ActivatedFeature ActiveFeature1 = ActivatedFeatureFactory.GetActivatedFeature(
                  Id,
                  Locations.ActivatedRootWeb.Guid,
                  referenceFeature,
                  Faulty, null, DateTime.Now,
                  Version
                  );

            referenceFeature.ToggleActivatedFeature(ActiveFeature1, true);

            // Act
            var notEqualFeatureId = FeatureDefinitionFactory.GetFeatureDefinition(
                            IdDifferent, CompatibilityLevel,
                            Description,
                            DisplayName, Hidden,
                            Name, Properties,
                            Scope,
                            Title,
                            SolutionId, UiVersion,
                            Version, DefinitioninstallationScope
                           );

            var notEqualFeatureCompatibility = FeatureDefinitionFactory.GetFeatureDefinition(
                          Id, CompatibilityLevelDifferent,
                           Description,
                          DisplayName, Hidden,
                          Name, Properties,
                          Scope,
                          Title,
                          SolutionId, UiVersion,
                          Version, DefinitioninstallationScope
                         );

            var notEqualFeatureScope = FeatureDefinitionFactory.GetFeatureDefinition(
                         Id, CompatibilityLevel,
                          Description,
                         DisplayName, Hidden,
                         Name, Properties,
                         ScopeDifferent,
                         Title,
                         SolutionId, UiVersion,
                         Version, DefinitioninstallationScope
                        );

            var notEqualFeatureName = FeatureDefinitionFactory.GetFeatureDefinition(
                          Id, CompatibilityLevel,
                           Description,
                          DisplayName, Hidden,
                          NameDifferent, Properties,
                          Scope,
                          Title,
                          SolutionId, UiVersion,
                          Version, DefinitioninstallationScope
                         );

            var notEqualFeatureDefInstScope = FeatureDefinitionFactory.GetFeatureDefinition(
                          Id, CompatibilityLevel,
                           Description,
                          DisplayName, Hidden,
                          Name, Properties,
                          Scope,
                          Title,
                          SolutionId, UiVersion,
                          Version, DefinitioninstallationScopeDifferent
                         );

            // Assert

            Assert.NotEqual(referenceFeature, notEqualFeatureId);
            Assert.NotEqual(referenceFeature, notEqualFeatureCompatibility);
            Assert.NotEqual(referenceFeature, notEqualFeatureName);
            Assert.NotEqual(referenceFeature, notEqualFeatureScope);
            Assert.NotEqual(referenceFeature, notEqualFeatureDefInstScope);


        }

    }
}

