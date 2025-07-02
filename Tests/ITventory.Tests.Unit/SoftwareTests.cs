using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using System.Linq;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class SoftwareTests
    {
        private readonly Guid _publisherId = Guid.NewGuid();
        private readonly ApprovalType _approvalType = ApprovalType.Approved;
        private readonly string _name = "Test Software";

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateSoftware()
        {
            // Act
            var software = new Software(_publisherId, _approvalType, _name);

            // Assert
            Assert.NotEqual(Guid.Empty, software.Id);
            Assert.Equal(_publisherId, software.PublisherId);
            Assert.Equal(_approvalType, software.ApprovalType);
            Assert.Equal(_name, software.Name);
            Assert.Empty(software.SoftwareVersions);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentException(string invalidName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Software(_publisherId, _approvalType, invalidName));
        }

        [Fact]
        public void Create_Should_Return_New_Software()
        {
            // Act
            var software = Software.Create(_publisherId, _approvalType, _name);

            // Assert
            Assert.NotNull(software);
            Assert.Equal(_name, software.Name);
        }

        [Fact]
        public void AddVersion_Should_Add_SoftwareVersion()
        {
            // Arrange
            var software = new Software(_publisherId, _approvalType, _name);
            var version = new SoftwareVersion("1.0", false, "some details", "path/to/file");

            // Act
            software.AddVersion(version);

            // Assert
            Assert.Single(software.SoftwareVersions);
            Assert.Equal(version, software.SoftwareVersions.First());
        }

        [Fact]
        public void AddVersion_With_Existing_Version_Should_Throw_InvalidOperationException()
        {
            // Arrange
            var software = new Software(_publisherId, _approvalType, _name);
            var version = new SoftwareVersion("1.0", false, "some details", "path/to/file");
            software.AddVersion(version);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => software.AddVersion(version));
        }

        [Fact]
        public void SetDefaultVersion_Should_Set_IsDefault_To_True_For_Correct_Version()
        {
            // Arrange
            var software = new Software(_publisherId, _approvalType, _name);
            var version1 = new SoftwareVersion("1.0", false, "details1", "path1");
            var version2 = new SoftwareVersion("2.0", false, "details2", "path2");
            software.AddVersion(version1);
            software.AddVersion(version2);

            // Act
            software.SetDefaultVersion(version2.Id);

            // Assert
            Assert.False(version1.IsDefault);
            Assert.True(version2.IsDefault);
            Assert.Equal(version2.Id, software.DefaultVersion);
        }

        [Fact]
        public void SetDefaultVersion_With_Nonexistent_Version_Should_Throw_InvalidOperationException()
        {
            // Arrange
            var software = new Software(_publisherId, _approvalType, _name);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => software.SetDefaultVersion(Guid.NewGuid()));
        }
    }
}
