using ITventory.Domain;
using ITventory.Domain.Entities;
using ITventory.Domain.Enums;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class HardwareTests
    {
        private readonly Hardware _hardware;
        private readonly Guid _primaryUserId = Guid.NewGuid();
        private readonly Region _defaultDomain = Region.APAC;
        private readonly HardwareType _hardwareType = HardwareType.Laptop;
        private readonly string _description = "Test Laptop";
        private readonly double _worth = 1500.00;
        private readonly Guid _producentId = Guid.NewGuid();
        private readonly Guid _modelId = Guid.NewGuid();
        private readonly int _modelYear = 2023;
        private readonly string _serialNumber = "SN12345";
        private readonly DateOnly _purchasedDate = new DateOnly(2023, 1, 1);
        private readonly Guid _roomId = Guid.NewGuid();
        private readonly Guid _departmentId = Guid.NewGuid();

        public HardwareTests()
        {
            _hardware = new Hardware(_primaryUserId, _defaultDomain, _hardwareType, _description, _worth, _producentId, _modelId, _modelYear, _serialNumber, _purchasedDate, _roomId, _departmentId);
        }

        [Fact]
        public void Constructor_Should_Initialize_Properties()
        {
            // Assert
            Assert.NotEqual(Guid.Empty, _hardware.Id);
            Assert.Equal(_primaryUserId, _hardware.PrimaryUserId);
            Assert.Equal(_defaultDomain, _hardware.DefaultDomain);
            Assert.Equal(_hardwareType, _hardware.HardwareType);
            Assert.True(_hardware.IsActive);
            Assert.Equal(_description, _hardware.Description);
            Assert.Equal(_worth, _hardware.Worth);
            Assert.Equal(_producentId, _hardware.ProducentId);
            Assert.Equal(_modelId, _hardware.ModelId);
            Assert.Equal(_modelYear, _hardware.ModelYear);
            Assert.Equal(_serialNumber, _hardware.SerialNumber);
            Assert.Equal(_purchasedDate, _hardware.PurchasedDate);
            Assert.Equal(_roomId, _hardware.RoomId);
            Assert.Equal(_departmentId, _hardware.DepartmentId);
        }

        [Fact]
        public void Create_Should_Return_New_Hardware()
        {
            // Act
            var hardware = Hardware.Create(_primaryUserId, _defaultDomain, _hardwareType, _description, _worth, _producentId, _modelId, _modelYear, _serialNumber, _purchasedDate, _roomId, _departmentId);

            // Assert
            Assert.NotNull(hardware);
            Assert.NotEqual(Guid.Empty, hardware.Id);
            Assert.Equal(_primaryUserId, hardware.PrimaryUserId);
        }

        [Fact]
        public void AddLogon_Should_Add_Logon_To_History()
        {
            // Arrange
            var logon = new Logon(Guid.NewGuid(), "test.user", _defaultDomain, DateTime.Now, _hardware.Id);

            // Act
            _hardware.AddLogon(logon);

            // Assert
            Assert.Single(_hardware._historyOfLogons);
            Assert.Equal(logon, _hardware._historyOfLogons[0]);
        }

        [Fact]
        public void TopUser_Should_Return_Most_Frequent_User()
        {
            // Arrange
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            _hardware.AddLogon(new Logon(user1, "user1", _defaultDomain, DateTime.Now, _hardware.Id));
            _hardware.AddLogon(new Logon(user2, "user2", _defaultDomain, DateTime.Now, _hardware.Id));
            _hardware.AddLogon(new Logon(user1, "user1", _defaultDomain, DateTime.Now, _hardware.Id));

            // Act
            var topUser = _hardware.TopUser;

            // Assert
            Assert.Equal(user1, topUser);
        }

        [Fact]
        public void Activate_Should_Set_IsActive_To_True()
        {
            // Arrange
            _hardware.Deactivate();

            // Act
            _hardware.Activate();

            // Assert
            Assert.True(_hardware.IsActive);
        }

        [Fact]
        public void Deactivate_Should_Set_IsActive_To_False()
        {
            // Act
            _hardware.Deactivate();

            // Assert
            Assert.False(_hardware.IsActive);
        }

        [Fact]
        public void Update_Should_Change_Properties()
        {
            // Arrange
            var newDescription = "Updated Laptop";
            var newWorth = 1600.00;
            var newProducentId = Guid.NewGuid();
            var newModelId = Guid.NewGuid();
            var newModelYear = 2024;
            var newSerialNumber = "SN54321";
            var newPurchasedDate = new DateOnly(2024, 1, 1);
            var newRoomId = Guid.NewGuid();
            var newDepartmentId = Guid.NewGuid();

            // Act
            _hardware.Update(newDescription, newWorth, newProducentId, newModelId, newModelYear, newSerialNumber, newPurchasedDate, newRoomId, newDepartmentId);

            // Assert
            Assert.Equal(newDescription, _hardware.Description);
            Assert.Equal(newWorth, _hardware.Worth);
            Assert.Equal(newProducentId, _hardware.ProducentId);
            Assert.Equal(newModelId, _hardware.ModelId);
            Assert.Equal(newModelYear, _hardware.ModelYear);
            Assert.Equal(newSerialNumber, _hardware.SerialNumber);
            Assert.Equal(newPurchasedDate, _hardware.PurchasedDate);
            Assert.Equal(newRoomId, _hardware.RoomId);
            Assert.Equal(newDepartmentId, _hardware.DepartmentId);
        }
    }
}
