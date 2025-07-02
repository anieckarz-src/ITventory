using ITventory.Domain;
using ITventory.Domain.ValueObjects;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class OfficeTests
    {
        private readonly string _street = "Test Street";
        private readonly string _buildingNumber = "123";
        private readonly Guid _locationId = Guid.NewGuid();
        private readonly Latitude _latitude = new Latitude(52.2297);
        private readonly Longitude _longitude = new Longitude(21.0122);

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateOffice()
        {
            // Act
            var office = new Office(_street, _buildingNumber, _locationId, _latitude, _longitude);

            // Assert
            Assert.NotEqual(Guid.Empty, office.Id);
            Assert.Equal(_street, office.Street);
            Assert.Equal(_buildingNumber, office.BuildingNumber);
            Assert.Equal(_locationId, office.LocationId);
            Assert.Equal(_latitude, office.Latitude);
            Assert.Equal(_longitude, office.Longitude);
            Assert.True(office.IsActive);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidStreet_ShouldThrowArgumentNullException(string invalidStreet)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Office(invalidStreet, _buildingNumber, _locationId, _latitude, _longitude));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidBuildingNumber_ShouldThrowArgumentNullException(string invalidBuildingNumber)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Office(_street, invalidBuildingNumber, _locationId, _latitude, _longitude));
        }

        [Fact]
        public void Create_Should_Return_New_Office()
        {
            // Act
            var office = Office.Create(_street, _buildingNumber, _locationId, _latitude, _longitude);

            // Assert
            Assert.NotNull(office);
            Assert.Equal(_street, office.Street);
        }

        [Fact]
        public void Deactivate_Should_Set_IsActive_To_False()
        {
            // Arrange
            var office = new Office(_street, _buildingNumber, _locationId, _latitude, _longitude);

            // Act
            office.Deactivate();

            // Assert
            Assert.False(office.IsActive);
        }

        [Fact]
        public void Deactivate_When_Already_Inactive_Should_Throw_Exception()
        {
            // Arrange
            var office = new Office(_street, _buildingNumber, _locationId, _latitude, _longitude);
            office.Deactivate();

            // Act & Assert
            Assert.Throws<Exception>(() => office.Deactivate());
        }
    }
}
