using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class LogonTests
    {
        private readonly Guid _hardwareId = Guid.NewGuid();
        private readonly Guid _userId = Guid.NewGuid();
        private readonly Region _domain = Region.EMEA;
        private readonly DateTime _logonTime = DateTime.UtcNow;
        private readonly string _ipAddress = "192.168.1.100";

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateLogon()
        {
            // Act
            var logon = new Logon(_hardwareId, _userId, _domain, _logonTime, _ipAddress);

            // Assert
            Assert.NotEqual(Guid.Empty, logon.Id);
            Assert.Equal(_hardwareId, logon.HardwareId);
            Assert.Equal(_userId, logon.UserId);
            Assert.Equal(_domain, logon.Domain);
            Assert.Equal(_logonTime, logon.LogonTime);
            Assert.Equal(_ipAddress, logon.IpAddress);
        }

        [Fact]
        public void Constructor_WithInvalidDomain_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidDomain = (Region)999;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Logon(_hardwareId, _userId, invalidDomain, _logonTime, _ipAddress));
        }

        [Fact]
        public void Constructor_WithEmptyUserId_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Logon(_hardwareId, Guid.Empty, _domain, _logonTime, _ipAddress));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidIpAddress_ShouldThrowArgumentException(string invalidIpAddress)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Logon(_hardwareId, _userId, _domain, _logonTime, invalidIpAddress));
        }

        [Fact]
        public void Create_Should_Return_New_Logon()
        {
            // Act
            var logon = Logon.Create(_hardwareId, _userId, _domain, _logonTime, _ipAddress);

            // Assert
            Assert.NotNull(logon);
            Assert.Equal(_userId, logon.UserId);
        }
    }
}
