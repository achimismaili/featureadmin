using FeatureAdmin.Core.Models;
using System;
using Xunit;

namespace FeatureAdmin.Core.Tests.Messages
{
    public class ItemUpdatedTests
    {
        
        [Fact]
        public void ItemMustNotBeNull()
        {

            // Arrange
            Location l = null;
            // Act

            Action act = () => new Core.Messages.Completed.ItemUpdated<Location>(Guid.NewGuid(), l);

            // Assert
            Assert.Throws<ArgumentNullException>(act);

        }

    }
}

