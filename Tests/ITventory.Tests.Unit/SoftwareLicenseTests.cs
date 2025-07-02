using ITventory.Domain;
using ITventory.Domain.Entities.JoinTables;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class SoftwareLicenseTests
    {
        private readonly LicenseType _licenseType = LicenseType.PerUser;
        private readonly string _licenseKey = "VALID-KEY";
        private readonly DateOnly _validUntil = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1));
        private readonly int _maxUse = 10;
        private readonly Guid _softwareVersion = Guid.NewGuid();

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateLicense()
        {
            // Act
            var license = new SoftwareLicense(_licenseType, _licenseKey, _validUntil, _maxUse, _softwareVersion);

            // Assert
            Assert.NotEqual(Guid.Empty, license.Id);
            Assert.Equal(_licenseType, license.LicenseType);
            Assert.Equal(_licenseKey, license.LicenseKey);
            Assert.Equal(_validUntil, license.ValidUntil);
            Assert.Equal(_maxUse, license.MaxUse);
            Assert.Equal(_softwareVersion, license.SoftwareVersion);
        }

        [Fact]
        public void Create_Should_Return_New_License()
        {
            // Act
            var license = SoftwareLicense.Create(_licenseType, _licenseKey, _validUntil, _maxUse, _softwareVersion);

            // Assert
            Assert.NotNull(license);
            Assert.Equal(_licenseKey, license.LicenseKey);
        }

        [Fact]
        public void AssignToUser_Should_Assign_License_To_User()
        {
            // Arrange
            var license = new SoftwareLicense(LicenseType.PerUser, _licenseKey, _validUntil, 1, _softwareVersion);
            var user = Employee.CreateMinimal(new Username("testuser"), "identity");

            // Act
            license.AssignToUser(user);

            // Assert
            Assert.Single(license.AssignedUsers);
            Assert.Equal(user.Id, license.AssignedUsers.First().EmployeeId);
        }

        [Fact]
        public void AssignToUser_When_Expired_Should_Throw_InvalidOperationException()
        {
            // Arrange
            var expiredLicense = new SoftwareLicense(LicenseType.PerUser, _licenseKey, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)), 1, _softwareVersion);
            var user = Employee.CreateMinimal(new Username("testuser"), "identity");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => expiredLicense.AssignToUser(user));
        }

        [Fact]
        public void AssignToUser_With_Wrong_LicenseType_Should_Throw_ArgumentException()
        {
            // Arrange
            var license = new SoftwareLicense(LicenseType.PerComputer, _licenseKey, _validUntil, 1, _softwareVersion);
            var user = Employee.CreateMinimal(new Username("testuser"), "identity");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => license.AssignToUser(user));
        }

        [Fact]
        public void AssignToUser_When_Maxed_Out_Should_Throw_InvalidOperationException()
        {
            // Arrange
            var license = new SoftwareLicense(LicenseType.PerUser, _licenseKey, _validUntil, 1, _softwareVersion);
            var user1 = Employee.CreateMinimal(new Username("user1"), "id1");
            var user2 = Employee.CreateMinimal(new Username("user2"), "id2");
            license.AssignToUser(user1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => license.AssignToUser(user2));
        }

        [Fact]
        public void AssignToUser_With_Already_Assigned_User_Should_Throw_InvalidOperationException()
        {
            // Arrange
            var license = new SoftwareLicense(LicenseType.PerUser, _licenseKey, _validUntil, 2, _softwareVersion);
            var user = Employee.CreateMinimal(new Username("testuser"), "identity");
            license.AssignToUser(user);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => license.AssignToUser(user));
        }

        [Fact]
        public void ReassignLicenseToUser_Should_Reassign_License()
        {
            // Arrange
            var license = new SoftwareLicense(LicenseType.PerUser, _licenseKey, _validUntil, 1, _softwareVersion);
            var oldUser = Employee.CreateMinimal(new Username("olduser"), "id1");
            var newUser = Employee.CreateMinimal(new Username("newuser"), "id2");
            license.AssignToUser(oldUser);

            // Act
            license.ReassignLicenseToUser(newUser, oldUser);

            // Assert
            Assert.DoesNotContain(license.AssignedUsers, a => a.EmployeeId == oldUser.Id);
            Assert.Contains(license.AssignedUsers, a => a.EmployeeId == newUser.Id);
        }

        [Fact]
        public void AssignToHardware_Should_Assign_License_To_Hardware()
        {
            // Arrange
            var license = new SoftwareLicense(LicenseType.PerComputer, _licenseKey, _validUntil, 1, _softwareVersion);
            var hardware = new Hardware(Guid.NewGuid(), Region.APAC, HardwareType.Laptop, "desc", 1, Guid.NewGuid(), Guid.NewGuid(), 2022, "sn", new DateOnly(2022,1,1), Guid.NewGuid(), Guid.NewGuid());

            // Act
            license.AssignToHardware(hardware);

            // Assert
            Assert.Single(license.AssignedHardware);
            Assert.Equal(hardware.Id, license.AssignedHardware.First().HardwareId);
        }
    }
}
