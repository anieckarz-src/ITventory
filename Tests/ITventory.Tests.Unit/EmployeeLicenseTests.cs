using ITventory.Domain.Entities.JoinTables;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class EmployeeLicenseTests
    {
        private readonly Guid _licenseId = Guid.NewGuid();
        private readonly Guid _employeeId = Guid.NewGuid();

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateEmployeeLicense()
        {
            // Act
            var employeeLicense = new EmployeeLicense(_licenseId, _employeeId);

            // Assert
            Assert.NotEqual(Guid.Empty, employeeLicense.Id);
            Assert.Equal(_licenseId, employeeLicense.LicenseId);
            Assert.Equal(_employeeId, employeeLicense.EmployeeId);
        }

        [Fact]
        public void Create_Should_Return_New_EmployeeLicense()
        {
            // Act
            var employeeLicense = EmployeeLicense.Create(_licenseId, _employeeId);

            // Assert
            Assert.NotNull(employeeLicense);
            Assert.Equal(_licenseId, employeeLicense.LicenseId);
            Assert.Equal(_employeeId, employeeLicense.EmployeeId);
        }
    }
}
