using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using System.Linq;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class EquipmentTests
    {
        private readonly Equipment _equipment;
        private readonly string _description = "Test Equipment";
        private readonly double _worth = 1000.00;
        private readonly Guid _producentId = Guid.NewGuid();
        private readonly Guid _modelId = Guid.NewGuid();
        private readonly int _modelYear = 2022;
        private readonly string _serialNumber = "SN67890";
        private readonly DateOnly _purchasedDate = new DateOnly(2022, 1, 1);
        private readonly Guid _roomId = Guid.NewGuid();
        private readonly Guid _departmentId = Guid.NewGuid();
        private readonly Condition _initialCondition = Condition.Good;

        public EquipmentTests()
        {
            _equipment = new Equipment(_description, _worth, _producentId, _modelId, _modelYear, _serialNumber, _purchasedDate, _roomId, _departmentId, _initialCondition);
        }

        [Fact]
        public void Constructor_Should_Initialize_Properties()
        {
            // Assert
            Assert.Equal(_description, _equipment.Description);
            Assert.Equal(_worth, _equipment.Worth);
            Assert.Equal(_producentId, _equipment.ProducentId);
            Assert.Equal(_modelId, _equipment.ModelId);
            Assert.Equal(_modelYear, _equipment.ModelYear);
            Assert.Equal(_serialNumber, _equipment.SerialNumber);
            Assert.Equal(_purchasedDate, _equipment.PurchasedDate);
            Assert.Equal(_roomId, _equipment.RoomId);
            Assert.Equal(_departmentId, _equipment.DepartmentId);
            Assert.Equal(_initialCondition, _equipment.Condition);
        }

        [Fact]
        public void Create_Should_Return_New_Equipment()
        {
            // Act
            var equipment = Equipment.Create(_description, _worth, _producentId, _modelId, _modelYear, _serialNumber, _purchasedDate, _roomId, _departmentId, _initialCondition);

            // Assert
            Assert.NotNull(equipment);
            Assert.Equal(_description, equipment.Description);
        }

        [Fact]
        public void AddReview_Should_Add_Review_To_History_And_Update_Condition()
        {
            // Arrange
            var newCondition = Condition.Average;
            var review = new Review(_equipment.Id, Guid.NewGuid(), "Damage report", DateOnly.FromDateTime(DateTime.UtcNow), newCondition);

            // Act
            _equipment.AddReview(review);

            // Assert
            Assert.Single(_equipment.HistoryOfReviews);
            Assert.Equal(review, _equipment.HistoryOfReviews.First());
            Assert.Equal(newCondition, _equipment.Condition);
        }

        [Fact]
        public void AddReview_With_Null_Review_Should_Throw_ArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _equipment.AddReview(null));
        }

        [Fact]
        public void AddReview_With_Existing_Review_Should_Throw_ArgumentException()
        {
            // Arrange
            var review = new Review(_equipment.Id, Guid.NewGuid(), "Initial review", DateOnly.FromDateTime(DateTime.UtcNow), Condition.Good);
            _equipment.AddReview(review);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _equipment.AddReview(review));
        }

        [Theory]
        [InlineData(Condition.Ideal, 1.0)]
        [InlineData(Condition.Good, 0.8)]
        [InlineData(Condition.Average, 0.6)]
        [InlineData(Condition.Poor, 0.4)]
        [InlineData(Condition.Damaged, 0.0)]
        public void CurrentWorth_Should_Be_Calculated_Based_On_Condition(Condition condition, double multiplier)
        {
            // Arrange
            var equipment = new Equipment(_description, _worth, _producentId, _modelId, _modelYear, _serialNumber, _purchasedDate, _roomId, _departmentId, condition);
            var expectedWorth = _worth * multiplier;

            // Act
            var currentWorth = equipment.CurrentWorth;

            // Assert
            Assert.Equal(expectedWorth, currentWorth);
        }

        [Fact]
        public void LastReviewed_Should_Return_Latest_Review_Date()
        {
            // Arrange
            var olderReviewDate = new DateOnly(2023, 1, 1);
            var newerReviewDate = new DateOnly(2023, 6, 1);
            var review1 = new Review(_equipment.Id, Guid.NewGuid(), "Older review", olderReviewDate, Condition.Good);
            var review2 = new Review(_equipment.Id, Guid.NewGuid(), "Newer review", newerReviewDate, Condition.Average);
            _equipment.AddReview(review1);
            _equipment.AddReview(review2);

            // Act
            var lastReviewed = _equipment.LastReviewed;

            // Assert
            Assert.Equal(newerReviewDate, lastReviewed);
        }

        [Fact]
        public void ReviewCount_Should_Return_Number_Of_Reviews()
        {
            // Arrange
            _equipment.AddReview(new Review(_equipment.Id, Guid.NewGuid(), "Review 1", DateOnly.FromDateTime(DateTime.UtcNow), Condition.Good));
            _equipment.AddReview(new Review(_equipment.Id, Guid.NewGuid(), "Review 2", DateOnly.FromDateTime(DateTime.UtcNow), Condition.Average));

            // Act
            var reviewCount = _equipment.ReviewCount;

            // Assert
            Assert.Equal(2, reviewCount);
        }
    }
}
