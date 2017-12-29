using FeatureAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Core.Tests.Common
{
    public static class TestData
    {
        public static class TestActivatedFeatures
        {
            public static ActivatedFeature GetNormalActivatedFeature(FeatureDefinition definition, Guid locationId)
            {
                return new ActivatedFeature(definition.Id,
                    locationId,
                    definition,
                            Constants.GenericValues.Faulty,
                            Constants.GenericValues.Properties,
                            Constants.GenericValues.TimeActivated,
                            Constants.GenericValues.Version);
            }
        }

        public static class TestFeatureDefinitions
        {
            public static class Ids
            {
                public static Guid Id001 = new Guid("fd000001-0000-0000-0000-000000000000");
                public static Guid Id002 = new Guid("fd000002-0000-0000-0000-000000000000");
                public static Guid Id003 = new Guid("fd000003-0000-0000-0000-000000000000");
                public static Guid Id004 = new Guid("fd000004-0000-0000-0000-000000000000");
                public static Guid Id005 = new Guid("fd000005-0000-0000-0000-000000000000");
                public static Guid Id006 = new Guid("fd000006-0000-0000-0000-000000000000");
                public static Guid Id007 = new Guid("fd000007-0000-0000-0000-000000000000");
                public static Guid Id008 = new Guid("fd000008-0000-0000-0000-000000000000");
                public static Guid Id009 = new Guid("fd000009-0000-0000-0000-000000000000");
                public static Guid Id010 = new Guid("fd000010-0000-0000-0000-000000000000");
            }

            public static IEnumerable<FeatureDefinition> GetFeatureDefinitions([NotNull] Guid[] definitionIds, Guid[] locationIdsOfActivatedFeatures = null)
            {
                var definitions = new List<FeatureDefinition>();

                foreach (Guid fdId in definitionIds)
                {
                    var fd = TestFeatureDefinitions.GetFeatureDefinition(fdId);

                    if (locationIdsOfActivatedFeatures != null)
                    {
                        foreach (Guid lId in locationIdsOfActivatedFeatures)
                        {
                            var feat = TestActivatedFeatures.GetNormalActivatedFeature(fd, lId);
                            fd.ToggleActivatedFeature(feat, true);
                        }
                    }
                    definitions.Add(fd);
                }
                return definitions;
            }

            public static FeatureDefinition GetFeatureDefinition(Guid definitionId)
            {
                return new FeatureDefinition(definitionId, Constants.GenericValues.CompatibilityLevel,
                            Constants.GenericValues.Description,
                            Constants.GenericValues.DisplayName,
                            Constants.GenericValues.Hidden,
                            Constants.GenericValues.Name,
                            Constants.GenericValues.Properties,
                            Constants.GenericValues.Scope,
                            Constants.GenericValues.Title,
                            Constants.GenericValues.SolutionId,
                            Constants.GenericValues.UiVersion,
                            Constants.GenericValues.Version);
            }
        }

        public static class TestLocations
        {
            public static class Ids
            {
                public static Guid Id001 = new Guid("ca000001-0000-0000-0000-000000000000");
                public static Guid Id002 = new Guid("ca000002-0000-0000-0000-000000000000");
                public static Guid Id003 = new Guid("ca000003-0000-0000-0000-000000000000");
                public static Guid Id004 = new Guid("ca000004-0000-0000-0000-000000000000");
                public static Guid Id005 = new Guid("ca000005-0000-0000-0000-000000000000");
                public static Guid Id006 = new Guid("ca000006-0000-0000-0000-000000000000");
                public static Guid Id007 = new Guid("ca000007-0000-0000-0000-000000000000");
                public static Guid Id008 = new Guid("ca000008-0000-0000-0000-000000000000");
                public static Guid Id009 = new Guid("ca000009-0000-0000-0000-000000000000");
                public static Guid Id010 = new Guid("ca000010-0000-0000-0000-000000000000");
            }

            public static IEnumerable<Location> GetLocations([NotNull] Guid[] locationIds, Guid[] activatedFeatureIds = null)
            {

                var locations = new List<Location>();


                foreach (Guid l in locationIds)
                {
                    var loc = new Location(l, Constants.GenericValues.DisplayName, Ids.Id010, Core.Models.Enums.Scope.Web,
                        Constants.GenericValues.Url, null, 0);

                    if (activatedFeatureIds != null)
                    {
                        foreach (Guid f in activatedFeatureIds)
                        {
                            var fd = TestFeatureDefinitions.GetFeatureDefinition(f);
                            var feat = TestActivatedFeatures.GetNormalActivatedFeature(fd, l);
                            loc.ToggleActivatedFeature(feat, true);
                        }
                    }

                    locations.Add(loc);
                }
                return locations;
            }
        }
    }
}
