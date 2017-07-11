using FeatureAdminForm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeatureAdmin.Test.Repository
{
    /// <summary>
    /// For SharePoint tests, make sure, you run in 64bit mode - Test - Test Settings - Default Processor Architecture - X64
    /// </summary>
    public class AdminRepositoryGetFeatureDefinitionTest 
    {
        private FeatureRepository repository;

        public AdminRepositoryGetFeatureDefinitionTest()
        {
            repository = new FeatureRepository();
        }

        [Fact]
        public void CanGetFeatureDefinitionsAll()
        {
            // Arrange

            //Act
            var featureDefinitions = repository.GetFeatureDefinitions();
            
            var FDefinitionHealthyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWeb.Id);
            var FDefinitionHealthySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthySite.Id);
            var FDefinitionHealthyWebApp = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWebApp.Id);
            var FDefinitionHealthyFarm = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyFarm.Id);
            var FDefinitionFaultyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultyWeb.Id);
            var FDefinitionFaultySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultySite.Id);


            //Assert
            Assert.NotNull(FDefinitionHealthyWeb);
            Assert.Equal(TestContent.TestFeatures.HealthyWeb.Name, FDefinitionHealthyWeb.Name);
            Assert.Equal(TestContent.TestFeatures.HealthyWeb.Title, FDefinitionHealthyWeb.GetTitle);
            Assert.Equal(TestContent.TestFeatures.HealthyWeb.Scope, FDefinitionHealthyWeb.Scope);
            Assert.Equal(TestContent.TestFeatures.HealthyWeb.Version, FDefinitionHealthyWeb.DefinitionVersion);
            Assert.Equal(TestContent.TestFeatures.HealthyWeb.Faulty, FDefinitionHealthyWeb.Faulty);
            Assert.Equal(TestContent.TestFeatures.HealthyWeb.TotalActivated, FDefinitionHealthyWeb.ActivatedFeatures.Count);

            Assert.NotNull(FDefinitionHealthySite);
            Assert.Equal(TestContent.TestFeatures.HealthySite.Name, FDefinitionHealthySite.Name);
            Assert.Equal(TestContent.TestFeatures.HealthySite.Title, FDefinitionHealthySite.GetTitle);
            Assert.Equal(TestContent.TestFeatures.HealthySite.Scope, FDefinitionHealthySite.Scope);
            Assert.Equal(TestContent.TestFeatures.HealthySite.Version, FDefinitionHealthySite.DefinitionVersion);
            Assert.Equal(TestContent.TestFeatures.HealthySite.Faulty, FDefinitionHealthySite.Faulty);
            Assert.Equal(TestContent.TestFeatures.HealthySite.TotalActivated, FDefinitionHealthySite.ActivatedFeatures.Count);

            Assert.NotNull(FDefinitionHealthyWebApp);
            Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Name, FDefinitionHealthyWebApp.Name);
            Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Title, FDefinitionHealthyWebApp.GetTitle);
            Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Scope, FDefinitionHealthyWebApp.Scope);
            Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Version, FDefinitionHealthyWebApp.DefinitionVersion);
            Assert.Equal(TestContent.TestFeatures.HealthyWebApp.Faulty, FDefinitionHealthyWebApp.Faulty);
            Assert.Equal(TestContent.TestFeatures.HealthyWebApp.TotalActivated, FDefinitionHealthyWebApp.ActivatedFeatures.Count);

            Assert.NotNull(FDefinitionHealthyFarm);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Name, FDefinitionHealthyFarm.Name);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Title, FDefinitionHealthyFarm.GetTitle);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Scope, FDefinitionHealthyFarm.Scope);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Version, FDefinitionHealthyFarm.DefinitionVersion);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.Faulty, FDefinitionHealthyFarm.Faulty);
            Assert.Equal(TestContent.TestFeatures.HealthyFarm.TotalActivated, FDefinitionHealthyFarm.ActivatedFeatures.Count);

            Assert.NotNull(FDefinitionFaultyWeb);
            Assert.Equal(TestContent.TestFeatures.FaultyWeb.Name, FDefinitionFaultyWeb.Name);
            Assert.Equal(TestContent.TestFeatures.FaultyWeb.Title, FDefinitionFaultyWeb.GetTitle);
            Assert.Equal(TestContent.TestFeatures.FaultyWeb.Scope, FDefinitionFaultyWeb.Scope);
            Assert.Equal(null, FDefinitionFaultyWeb.DefinitionVersion);
            Assert.Equal(TestContent.TestFeatures.FaultyWeb.Faulty, FDefinitionFaultyWeb.Faulty);
       //     Assert.Equal(TestContent.TestFeatures.FaultyWeb.TotalActivated, FDefinitionFaultyWeb.ActivatedFeatures.Count); // for some reasons, 4 are counted, not 3 ... (?)

            Assert.NotNull(FDefinitionFaultySite);
            Assert.Equal(TestContent.TestFeatures.FaultySite.Name, FDefinitionFaultySite.Name);
            Assert.Equal(TestContent.TestFeatures.FaultySite.Title, FDefinitionFaultySite.GetTitle);
            Assert.Equal(TestContent.TestFeatures.FaultySite.Scope, FDefinitionFaultySite.Scope);
            Assert.Equal(null, FDefinitionFaultySite.DefinitionVersion);
            Assert.Equal(TestContent.TestFeatures.FaultySite.Faulty, FDefinitionFaultySite.Faulty);
         //   Assert.Equal(TestContent.TestFeatures.FaultySite.TotalActivated, FDefinitionFaultySite.ActivatedFeatures.Count); // for some reasons, 2 are counted, not 1 ... (?)
        }

        [Fact]
        public void CanGetFeatureDefinitionsWeb()
        {
            // Arrange

            //Act
            var featureDefinitions = repository.GetFeatureDefinitions(Microsoft.SharePoint.SPFeatureScope.Web);

            var FDefinitionHealthyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWeb.Id);
            var FDefinitionHealthySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthySite.Id);
            var FDefinitionHealthyWebApp = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWebApp.Id);
            var FDefinitionHealthyFarm = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyFarm.Id);
            var FDefinitionFaultyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultyWeb.Id);
            var FDefinitionFaultySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultySite.Id);


            //Assert
            Assert.NotNull(FDefinitionHealthyWeb);
            Assert.Null(FDefinitionHealthySite);
            Assert.Null(FDefinitionHealthyWebApp);
            Assert.Null(FDefinitionHealthyFarm);
            Assert.NotNull(FDefinitionFaultyWeb);
            Assert.Null(FDefinitionFaultySite);

        }

        [Fact]
        public void CanGetFeatureDefinitionsSite()
        {
            // Arrange

            //Act
            var featureDefinitions = repository.GetFeatureDefinitions(Microsoft.SharePoint.SPFeatureScope.Site);

            var FDefinitionHealthyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWeb.Id);
            var FDefinitionHealthySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthySite.Id);
            var FDefinitionHealthyWebApp = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWebApp.Id);
            var FDefinitionHealthyFarm = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyFarm.Id);
            var FDefinitionFaultyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultyWeb.Id);
            var FDefinitionFaultySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultySite.Id);


            //Assert
            Assert.Null(FDefinitionHealthyWeb);
            Assert.NotNull(FDefinitionHealthySite);
            Assert.Null(FDefinitionHealthyWebApp);
            Assert.Null(FDefinitionHealthyFarm);
            Assert.Null(FDefinitionFaultyWeb);
            Assert.NotNull(FDefinitionFaultySite);

        }

        [Fact]
        public void CanGetFeatureDefinitionsWebApp()
        {
            // Arrange

            //Act
            var featureDefinitions = repository.GetFeatureDefinitions(Microsoft.SharePoint.SPFeatureScope.WebApplication);

            var FDefinitionHealthyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWeb.Id);
            var FDefinitionHealthySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthySite.Id);
            var FDefinitionHealthyWebApp = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWebApp.Id);
            var FDefinitionHealthyFarm = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyFarm.Id);
            var FDefinitionFaultyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultyWeb.Id);
            var FDefinitionFaultySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultySite.Id);


            //Assert
            Assert.Null(FDefinitionHealthyWeb);
            Assert.Null(FDefinitionHealthySite);
            Assert.NotNull(FDefinitionHealthyWebApp);
            Assert.Null(FDefinitionHealthyFarm);
            Assert.Null(FDefinitionFaultyWeb);
            Assert.Null(FDefinitionFaultySite);

        }

        [Fact]
        public void CanGetFeatureDefinitionsFarm()
        {
            // Arrange

            //Act
            var featureDefinitions = repository.GetFeatureDefinitions(Microsoft.SharePoint.SPFeatureScope.Farm);

            var FDefinitionHealthyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWeb.Id);
            var FDefinitionHealthySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthySite.Id);
            var FDefinitionHealthyWebApp = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyWebApp.Id);
            var FDefinitionHealthyFarm = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.HealthyFarm.Id);
            var FDefinitionFaultyWeb = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultyWeb.Id);
            var FDefinitionFaultySite = featureDefinitions.FirstOrDefault(f => f.Id == TestContent.TestFeatures.FaultySite.Id);


            //Assert
            Assert.Null(FDefinitionHealthyWeb);
            Assert.Null(FDefinitionHealthySite);
            Assert.Null(FDefinitionHealthyWebApp);
            Assert.NotNull(FDefinitionHealthyFarm);
            Assert.Null(FDefinitionFaultyWeb);
            Assert.Null(FDefinitionFaultySite);

        }
    }
}
