using ITventory.Domain;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class DepartmentTests
    {
        private readonly string _name = "Test Department";
        private readonly Guid _managerId = Guid.NewGuid();

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateDepartment()
        {
            // Act
            var department = new Department(_name, _managerId);

            // Assert
            Assert.NotEqual(Guid.Empty, department.Id);
            Assert.Equal(_name, department.Name);
            Assert.Equal(_managerId, department.ManagerId);
        }

        [Fact]
        public void Create_Should_Return_New_Department()
        {
            // Act
            var department = Department.Create(_name, _managerId);

            // Assert
            Assert.NotNull(department);
            Assert.Equal(_name, department.Name);
            Assert.Equal(_managerId, department.ManagerId);
        }

        [Fact]
        public void SetManager_Should_Update_ManagerId()
        {
            // Arrange
            var department = new Department(_name, _managerId);
            var newManagerId = Guid.NewGuid();

            // Act
            department.SetManager(newManagerId);

            // Assert
            Assert.Equal(newManagerId, department.ManagerId);
        }

        [Fact]
        public void SetManager_With_Empty_Guid_Should_Throw_ArgumentNullException()
        {
            // Arrange
            var department = new Department(_name, _managerId);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => department.SetManager(Guid.Empty));
        }
    }
}
