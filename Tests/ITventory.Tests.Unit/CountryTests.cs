using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class CountryTests
    {
        private readonly string _name = "Test Country";
        private readonly string _countryCode = "TC";
        private readonly Region _region = Region.EMEA;

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateCountry()
        {
            // Act
            var country = new Country(_name, _countryCode, _region);

            // Assert
            Assert.NotEqual(Guid.Empty, country.Id);
            Assert.Equal(_name, country.Name);
            Assert.Equal(_countryCode, country.CountryCode);
            Assert.Equal(_region, country.Region);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentNullException(string invalidName)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Country(invalidName, _countryCode, _region));
        }

        [Fact]
        public void Constructor_WithInvalidRegion_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var invalidRegion = (Region)999;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Country(_name, _countryCode, invalidRegion));
        }

        [Fact]
        public void Create_Should_Return_New_Country()
        {
            // Act
            var country = Country.Create(_name, _countryCode, _region);

            // Assert
            Assert.NotNull(country);
            Assert.Equal(_name, country.Name);
        }

        [Fact]
        public void SetRegulations_Should_Update_Regulations()
        {
            // Arrange
            var country = new Country(_name, _countryCode, _region);
            var regulations = "These are the new regulations.";

            // Act
            country.SetRegulations(regulations);

            // Assert
            Assert.Equal(regulations, country.Regulations);
        }

        [Fact]
        public void SetRegulations_With_Empty_String_Should_Throw_ArgumentNullException()
        {
            // Arrange
            var country = new Country(_name, _countryCode, _region);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => country.SetRegulations(""));
        }
    }
}
