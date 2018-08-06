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
            public static ActivatedFeature GetNormalActivatedFeature(FeatureDefinition definition, string locationId)
            {
                return new ActivatedFeature(
                    definition.UniqueIdentifier,
                    locationId,
                    definition.DisplayName,
                            Constants.GenericValues.Faulty,
                            Constants.GenericValues.Properties,
                            Constants.GenericValues.TimeActivated,
                            Constants.GenericValues.Version,
                            definition.Version
                            );
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
        }
    }
}
