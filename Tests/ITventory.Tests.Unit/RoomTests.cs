using ITventory.Domain;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class RoomTests
    {
        private readonly Guid _officeId = Guid.NewGuid();
        private readonly string _roomName = "Conference Room A";
        private readonly int _floor = 1;
        private readonly double _area = 50.0;
        private readonly int _capacity = 10;
        private readonly Guid _personResponsibleId = Guid.NewGuid();

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateRoom()
        {
            // Act
            var room = new Room(_officeId, _roomName, _floor, _area, _capacity, _personResponsibleId);

            // Assert
            Assert.NotEqual(Guid.Empty, room.Id);
            Assert.Equal(_officeId, room.OfficeId);
            Assert.Equal(_roomName, room.RoomName);
            Assert.Equal(_floor, room.Floor);
            Assert.Equal(_area, room.Area);
            Assert.Equal(_capacity, room.Capacity);
            Assert.Equal(_personResponsibleId, room.PersonResponsibleId);
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(11)]
        public void Constructor_WithInvalidFloor_ShouldThrowArgumentException(int invalidFloor)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Room(_officeId, _roomName, invalidFloor, _area, _capacity, _personResponsibleId));
        }

        [Theory]
        [InlineData(4)]
        [InlineData(2001)]
        public void Constructor_WithInvalidArea_ShouldThrowArgumentException(double invalidArea)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Room(_officeId, _roomName, _floor, invalidArea, _capacity, _personResponsibleId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(101)]
        public void Constructor_WithInvalidCapacity_ShouldThrowArgumentException(int invalidCapacity)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Room(_officeId, _roomName, _floor, _area, invalidCapacity, _personResponsibleId));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidRoomName_ShouldThrowArgumentException(string invalidRoomName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Room(_officeId, invalidRoomName, _floor, _area, _capacity, _personResponsibleId));
        }

        [Fact]
        public void Create_Should_Return_New_Room()
        {
            // Act
            var room = Room.Create(_officeId, _roomName, _floor, _area, _capacity, _personResponsibleId);

            // Assert
            Assert.NotNull(room);
            Assert.Equal(_roomName, room.RoomName);
        }

        [Fact]
        public void AssignToRoom_Should_Add_Employee_To_Room()
        {
            // Arrange
            var room = new Room(_officeId, _roomName, _floor, _area, _capacity, _personResponsibleId);
            var employee = Employee.CreateMinimal(new Username("testuser"), "identity");

            // Act
            room.AssignToRoom(employee);

            // Assert
            Assert.Single(room.Employees);
            Assert.Equal(employee, room.Employees.First());
        }

        [Fact]
        public void AssignToRoom_With_Null_Employee_Should_Throw_ArgumentNullException()
        {
            // Arrange
            var room = new Room(_officeId, _roomName, _floor, _area, _capacity, _personResponsibleId);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => room.AssignToRoom(null));
        }

        [Fact]
        public void AssignToRoom_When_Capacity_Exceeded_Should_Throw_InvalidOperationException()
        {
            // Arrange
            var room = new Room(_officeId, _roomName, _floor, _area, 1, _personResponsibleId);
            var employee1 = Employee.CreateMinimal(new Username("user1"), "id1");
            var employee2 = Employee.CreateMinimal(new Username("user2"), "id2");
            room.AssignToRoom(employee1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => room.AssignToRoom(employee2));
        }

        [Fact]
        public void RemoveFromRoom_Should_Remove_Employee_From_Room()
        {
            // Arrange
            var room = new Room(_officeId, _roomName, _floor, _area, _capacity, _personResponsibleId);
            var employee = Employee.CreateMinimal(new Username("testuser"), "identity");
            room.AssignToRoom(employee);

            // Act
            room.RemoveFromRoom(employee);

            // Assert
            Assert.Empty(room.Employees);
        }

        [Fact]
        public void RemoveFromRoom_With_Employee_Not_In_Room_Should_Throw_InvalidOperationException()
        {
            // Arrange
            var room = new Room(_officeId, _roomName, _floor, _area, _capacity, _personResponsibleId);
            var employee = Employee.CreateMinimal(new Username("testuser"), "identity");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => room.RemoveFromRoom(employee));
        }

        [Fact]
        public void AddInventory_Should_Add_Product_To_Room_Inventory()
        {
            // Arrange
            var room = new Room(_officeId, _roomName, _floor, _area, _capacity, _personResponsibleId);
            var product = new Product("Test Product", ProductType.Consumable, 100.00, 50);
            var sku = 10;

            // Act
            room.AddInventory(product, sku);

            // Assert
            Assert.Single(room.RoomInventory);
            Assert.Equal(product, room.RoomInventory.First().Product);
            Assert.Equal(sku, room.RoomInventory.First().SKU);
        }
    }
}
