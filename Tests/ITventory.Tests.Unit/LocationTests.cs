using ITventory.Domain;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class LocationTests
    {
        private readonly string _name = "Test Location";
        private readonly Guid _countryId = Guid.NewGuid();
        private readonly ZipCode _zipCode = new ZipCode("12-345");
        private readonly string _city = "Test City";
        private readonly Latitude _latitude = new Latitude(52.2297);
        private readonly Longitude _longitude = new Longitude(21.0122);
        private readonly TypeOfPlant _typeOfPlant = TypeOfPlant.Office;

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateLocation()
        {
            // Act
            var location = new Location(_name, _countryId, _zipCode, _city, _latitude, _longitude, _typeOfPlant);

            // Assert
            Assert.NotEqual(Guid.Empty, location.Id);
            Assert.Equal(_name, location.Name);
            Assert.Equal(_countryId, location.CountryId);
            Assert.Equal(_zipCode, location.ZipCode);
            Assert.Equal(_city, location.City);
            Assert.Equal(_latitude, location.Latitude);
            Assert.Equal(_longitude, location.Longitude);
            Assert.Equal(_typeOfPlant, location.TypeOfPlant);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentNullException(string invalidName)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Location(invalidName, _countryId, _zipCode, _city, _latitude, _longitude, _typeOfPlant));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        public void Constructor_WithInvalidCity_ShouldThrowArgumentNullException(string invalidCity)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Location(_name, _countryId, _zipCode, invalidCity, _latitude, _longitude, _typeOfPlant));
        }

        [Fact]
        public void Constructor_WithInvalidTypeOfPlant_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidTypeOfPlant = (TypeOfPlant)999;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Location(_name, _countryId, _zipCode, _city, _latitude, _longitude, invalidTypeOfPlant));
        }

        [Fact]
        public void Create_Should_Return_New_Location()
        {
            // Act
            var location = Location.Create(_name, _countryId, _zipCode, _city, _latitude, _longitude, _typeOfPlant);

            // Assert
            Assert.NotNull(location);
            Assert.Equal(_name, location.Name);
        }
    }
}
