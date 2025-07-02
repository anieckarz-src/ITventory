using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class ReviewTests
    {
        private readonly Guid _reviewedEquipmentId = Guid.NewGuid();
        private readonly Guid _reviewerId = Guid.NewGuid();
        private readonly string _details = "Test review details";
        private readonly DateOnly _reviewDate = DateOnly.FromDateTime(DateTime.UtcNow);
        private readonly Condition _condition = Condition.Good;

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateReview()
        {
            // Act
            var review = new Review(_reviewedEquipmentId, _reviewerId, _details, _reviewDate, _condition);

            // Assert
            Assert.NotEqual(Guid.Empty, review.Id);
            Assert.Equal(_reviewedEquipmentId, review.ReviwedEquipmentId);
            Assert.Equal(_reviewerId, review.ReviewerId);
            Assert.Equal(_details, review.Details);
            Assert.Equal(_reviewDate, review.ReviewDate);
            Assert.Equal(_condition, review.Condition);
        }

        [Fact]
        public void Constructor_WithFutureDate_ShouldThrowArgumentException()
        {
            // Arrange
            var futureDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Review(_reviewedEquipmentId, _reviewerId, _details, futureDate, _condition));
        }

        [Fact]
        public void Constructor_WithInvalidCondition_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidCondition = (Condition)999; // Assuming 999 is not a valid Condition

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Review(_reviewedEquipmentId, _reviewerId, _details, _reviewDate, invalidCondition));
        }

        [Fact]
        public void Create_Should_Return_New_Review()
        {
            // Act
            var review = Review.Create(_reviewedEquipmentId, _reviewerId, _details, _reviewDate, _condition);

            // Assert
            Assert.NotNull(review);
            Assert.Equal(_reviewedEquipmentId, review.ReviwedEquipmentId);
            Assert.Equal(_reviewerId, review.ReviewerId);
        }
    }
}
