using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class SoftwareVersionTests
    {
        private readonly Guid _softwareId = Guid.NewGuid();
        private readonly string _versionNumber = "1.0.5";
        private readonly decimal _price = 99.99m;
        private readonly DateOnly _published = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30));
        private readonly LicenseType _licenseType = LicenseType.PerUser;

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateSoftwareVersion()
        {
            // Act
            var softwareVersion = new SoftwareVersion(_softwareId, _versionNumber, _price, _published, _licenseType);

            // Assert
            Assert.NotEqual(Guid.Empty, softwareVersion.Id);
            Assert.Equal(_softwareId, softwareVersion.SoftwareId);
            Assert.Equal(_versionNumber, softwareVersion.VersionNumber);
            Assert.Equal(_price, softwareVersion.Price);
            Assert.Equal(_published, softwareVersion.Published);
            Assert.Equal(_licenseType, softwareVersion.LicenseType);
            Assert.False(softwareVersion.IsDefault);
            Assert.False(softwareVersion.IsApproved);
            Assert.False(softwareVersion.IsActive);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("a")]
        [InlineData("")]
        public void Constructor_WithInvalidVersionNumber_ShouldThrowArgumentException(string invalidVersionNumber)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new SoftwareVersion(_softwareId, invalidVersionNumber, _price, _published, _licenseType));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void Constructor_WithInvalidPrice_ShouldThrowArgumentException(decimal invalidPrice)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new SoftwareVersion(_softwareId, _versionNumber, invalidPrice, _published, _licenseType));
        }

        [Fact]
        public void Constructor_WithFuturePublishedDate_ShouldThrowArgumentException()
        {
            // Arrange
            var futureDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new SoftwareVersion(_softwareId, _versionNumber, _price, futureDate, _licenseType));
        }

        [Fact]
        public void Create_Should_Return_New_SoftwareVersion()
        {
            // Act
            var softwareVersion = SoftwareVersion.Create(_softwareId, _versionNumber, _price, _published, _licenseType);

            // Assert
            Assert.NotNull(softwareVersion);
            Assert.Equal(_versionNumber, softwareVersion.VersionNumber);
        }

        [Fact]
        public void ApproveSoftware_Should_Set_IsApproved_And_IsActive_To_True()
        {
            // Arrange
            var softwareVersion = new SoftwareVersion(_softwareId, _versionNumber, _price, _published, _licenseType);

            // Act
            softwareVersion.ApproveSoftware();

            // Assert
            Assert.True(softwareVersion.IsApproved);
            Assert.True(softwareVersion.IsActive);
        }

        [Fact]
        public void RetireSoftware_Should_Set_IsActive_To_False()
        {
            // Arrange
            var softwareVersion = new SoftwareVersion(_softwareId, _versionNumber, _price, _published, _licenseType);
            softwareVersion.ApproveSoftware();

            // Act
            softwareVersion.RetireSoftware();

            // Assert
            Assert.False(softwareVersion.IsActive);
        }

        [Fact]
        public void MakeDefault_Should_Set_IsDefault_To_True()
        {
            // Arrange
            var softwareVersion = new SoftwareVersion(_softwareId, _versionNumber, _price, _published, _licenseType);

            // Act
            softwareVersion.MakeDefault();

            // Assert
            Assert.True(softwareVersion.IsDefault);
        }

        [Fact]
        public void RemoveDefault_Should_Set_IsDefault_To_False()
        {
            // Arrange
            var softwareVersion = new SoftwareVersion(_softwareId, _versionNumber, _price, _published, _licenseType);
            softwareVersion.MakeDefault();

            // Act
            softwareVersion.RemoveDefault();

            // Assert
            Assert.False(softwareVersion.IsDefault);
        }

        [Fact]
        public void ChangePrice_Should_Update_Price()
        {
            // Arrange
            var softwareVersion = new SoftwareVersion(_softwareId, _versionNumber, _price, _published, _licenseType);
            var newPrice = 149.99m;

            // Act
            softwareVersion.ChangePrice(newPrice);

            // Assert
            Assert.Equal(newPrice, softwareVersion.Price);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void ChangePrice_WithInvalidPrice_ShouldThrowArgumentException(decimal invalidPrice)
        {
            // Arrange
            var softwareVersion = new SoftwareVersion(_softwareId, _versionNumber, _price, _published, _licenseType);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => softwareVersion.ChangePrice(invalidPrice));
        }
    }
}
