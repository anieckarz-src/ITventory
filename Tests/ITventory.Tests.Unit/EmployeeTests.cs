using ITventory.Domain;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class EmployeeTests
    {
        [Fact]
        public void CreateMinimal_WithValidArguments_ShouldCreateEmployee()
        {
            // Arrange
            var username = new Username("testuser");
            var identityId = "some-identity-id";

            // Act
            var employee = Employee.CreateMinimal(username, identityId);

            // Assert
            Assert.NotNull(employee);
            Assert.NotEqual(Guid.Empty, employee.Id);
            Assert.Equal(username, employee.Username);
            Assert.Equal(identityId, employee.IdentityId);
            Assert.True(employee.IsActive);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateMinimal_WithInvalidUsername_ShouldThrowArgumentException(string invalidUsername)
        {
            // Arrange
            var identityId = "some-identity-id";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Employee.CreateMinimal(new Username(invalidUsername), identityId));
        }

        [Fact]
        public void SetDetails_WithValidDetails_ShouldUpdateEmployeeProperties()
        {
            // Arrange
            var employee = Employee.CreateMinimal(new Username("testuser"), "some-identity-id");
            var name = "John";
            var lastName = "Doe";
            var area = Area.IT;
            var positionName = "Software Engineer";
            var seniority = Seniority.Mid;
            var managerId = Guid.NewGuid();
            var departmentId = Guid.NewGuid();
            var hireDate = new DateOnly(2022, 1, 1);
            var birthDate = new DateOnly(1990, 1, 1);
            var roomId = Guid.NewGuid();

            // Act
            employee.SetDetails(name, lastName, area, positionName, seniority, managerId, departmentId, hireDate, birthDate, roomId);

            // Assert
            Assert.Equal(name, employee.Name);
            Assert.Equal(lastName, employee.LastName);
            Assert.Equal(area, employee.Area);
            Assert.Equal(positionName, employee.PositionName);
            Assert.Equal(seniority, employee.Seniority);
            Assert.Equal(managerId, employee.ManagerId);
            Assert.Equal(departmentId, employee.DepartmentId);
            Assert.Equal(hireDate, employee.HireDate);
            Assert.Equal(birthDate, employee.BirthDate);
            Assert.Equal(roomId, employee.RoomId);
        }

        [Fact]
        public void Experience_ShouldBeCalculatedBasedOnHireDate()
        {
            // Arrange
            var employee = Employee.CreateMinimal(new Username("testuser"), "some-identity-id");
            var hireDate = new DateOnly(DateTime.UtcNow.Year - 5, 1, 1);
            employee.SetDetails("John", "Doe", Area.IT, "SE", Seniority.Mid, Guid.NewGuid(), Guid.NewGuid(), hireDate, new DateOnly(1990,1,1), Guid.NewGuid());

            // Act
            var experience = employee.Experience;

            // Assert
            Assert.Equal(5, experience);
        }

        [Fact]
        public void Activate_ShouldSetIsActiveToTrue()
        {
            // Arrange
            var employee = Employee.CreateMinimal(new Username("testuser"), "some-identity-id");
            employee.Deactivate();

            // Act
            employee.Activate();

            // Assert
            Assert.True(employee.IsActive);
        }

        [Fact]
        public void Deactivate_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var employee = Employee.CreateMinimal(new Username("testuser"), "some-identity-id");

            // Act
            employee.Deactivate();

            // Assert
            Assert.False(employee.IsActive);
        }
    }
}
