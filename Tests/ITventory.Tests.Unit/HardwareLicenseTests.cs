using ITventory.Domain.Entities.JoinTables;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class HardwareLicenseTests
    {
        private readonly Guid _licenseId = Guid.NewGuid();
        private readonly Guid _hardwareId = Guid.NewGuid();

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateHardwareLicense()
        {
            // Act
            var hardwareLicense = new HardwareLicense(_licenseId, _hardwareId);

            // Assert
            Assert.NotEqual(Guid.Empty, hardwareLicense.Id);
            Assert.Equal(_licenseId, hardwareLicense.LicenseId);
            Assert.Equal(_hardwareId, hardwareLicense.HardwareId);
        }

        [Fact]
        public void Create_Should_Return_New_HardwareLicense()
        {
            // Act
            var hardwareLicense = HardwareLicense.Create(_licenseId, _hardwareId);

            // Assert
            Assert.NotNull(hardwareLicense);
            Assert.Equal(_licenseId, hardwareLicense.LicenseId);
            Assert.Equal(_hardwareId, hardwareLicense.HardwareId);
        }
    }
}
