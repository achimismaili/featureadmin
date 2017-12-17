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
    public class ItemUpdatedTests
    {
        
        [Fact]
        public void ItemMustNotBeNull()
        {

            // Arrange
            Location l = null;
            // Act

            Action act = () => new Messages.ItemUpdated<Location>(Guid.NewGuid(), l);

            // Assert
            Assert.Throws<ArgumentNullException>(act);

        }

    }
}

