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
    public class ActivatedFeatureEqualTests
    {
        public static Guid Id = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static Guid SolutionId = new Guid("5a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static string Name = "DummyFeaturesHealthy15_HealthyWeb";
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

        public static Guid IdDifferent = new Guid("6a5615a2-4c44-40dd-ac9f-26cc45fb7e78");
        public static Guid SolutionIdDifferent = new Guid("4a5615a2-4c44-40dd-ac9f-26cc45fb7e79");
        public static string NameDifferent = "DummyFeaturesHealthy15_HealthyWeb1";
        public static string DisplayNameDifferent = "Different Test Display Name";
        public static string DescriptionDifferent = "Different Test description";
        public static bool HiddenDifferent = !Hidden;
        public static FeatureDefinitionScope DefinitioninstallationScopeDifferent = FeatureDefinitionScope.None;
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
        // FeatureId, LocationId
        public void ActiveFeaturesWithSameFeatureIdAndLocationIdAreEqual()
        {
            // Arrange
            var referenceFeatureDefinition = FeatureDefinitionFactory.GetFeatureDefinition(
                            Id, CompatibilityLevel,
                             Description,
                            DisplayName, Hidden,
                            Name, Properties,
                            Scope,
                            Title,
                            SolutionId, UiVersion,
                            Version
                           );

            ActivatedFeature referenceFeature = ActivatedFeatureFactory.GetActivatedFeature(
                  referenceFeatureDefinition.UniqueIdentifier,
                  Locations.ActivatedRootWeb.Guid.ToString(),
                  referenceFeatureDefinition.DisplayName,
                  Faulty, Properties, TimeActivated,
                  Version, referenceFeatureDefinition.Version
                  );



            // Act

            var definitionDifferent = FeatureDefinitionFactory.GetFeatureDefinition(
                   Id, CompatibilityLevel,
                    null,
                   null, HiddenDifferent,
                   Name, null,
                   Scope,
                   null,
                   Guid.Empty, null,
                   null, 
                   null
                  );

            var equalFeature = ActivatedFeatureFactory.GetActivatedFeature(
                 definitionDifferent.UniqueIdentifier,
                 Locations.ActivatedRootWeb.Guid.ToString(),
                 definitionDifferent.DisplayName,
                 FaultyDifferent, 
                 PropertiesDifferent, 
                 TimeActivatedDifferent,
                 VersionDifferent, 
                 definitionDifferent.Version, 
                 DefinitioninstallationScopeDifferent
                 );

            var equalFeatureEmpty = ActivatedFeatureFactory.GetActivatedFeature(
                 Id.ToString(),
                 Locations.ActivatedRootWeb.Guid.ToString(),
                 null,
                 FaultyDifferent, null, DateTime.MinValue,
                 null, null
                 );

            // Assert

            Assert.Equal(referenceFeature, equalFeature);

            Assert.Equal(referenceFeature, equalFeatureEmpty);


        }

        [Fact]
        // Following needs to be equal:
        // FeatureId, LocationId
        public void ActiveFeaturesWithDifferentFeatureIdOrLocationIdAreNotEqual()
        {



            // Arrange
            var referenceFeatureDefinition = FeatureDefinitionFactory.GetFeatureDefinition(
                          Id, CompatibilityLevel,
                           Description,
                          DisplayName, Hidden,
                          Name, Properties,
                          Scope,
                          Title,
                          SolutionId, UiVersion,
                          Version
                         );

            ActivatedFeature referenceFeature = ActivatedFeatureFactory.GetActivatedFeature(
                  referenceFeatureDefinition.UniqueIdentifier,
                  SolutionId.ToString(),
                  referenceFeatureDefinition.DisplayName,
                  Faulty, Properties, TimeActivated,
                  Version, referenceFeatureDefinition.Version
                  );


            // Act
            var notEqualFeatureId = ActivatedFeatureFactory.GetActivatedFeature(
                  IdDifferent.ToString(),
                  SolutionId.ToString(),
                  referenceFeatureDefinition.DisplayName,
                  Faulty, Properties, TimeActivated,
                  Version, referenceFeatureDefinition.Version
                  );

            var notEqualLocationId = ActivatedFeatureFactory.GetActivatedFeature(
                  referenceFeatureDefinition.UniqueIdentifier,
                  SolutionIdDifferent.ToString(),
                  referenceFeatureDefinition.DisplayName,
                  Faulty, Properties, TimeActivated,
                  Version, referenceFeatureDefinition.Version
                  );

            // Assert

            Assert.NotEqual(referenceFeature, notEqualFeatureId);
            Assert.NotEqual(referenceFeature, notEqualLocationId);
        }

    }
}

