using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeatureAdmin.Core.Tests.Factories
{
    public class FeatureDefinitionFactoryTests
    {
        [Fact]
        // different feature definitions must be separate item in collection
        public void FactoryTransponseCorrectlySeparatesDefinitions()
        {
        }

        [Fact]
        // same feature definitions must be one item in collection with multiple activated features
        public void FactoryTransponseCorrectlyJoinsDefinitions()
        {
        }

        [Fact]
        // different feature definitions must be separate item in collection
        public void FactoryAddLoadedCorrectlySeparatesDefinitions()
        {
        }

        [Fact]
        // same feature definitions must be one item in collection with multiple activated features
        public void FactoryAddLoadedCorrectlyJoinsDefinitions()
        {
        }
    }
}
