using ITventory.Domain.Entities;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class RatingSoftwareVersionTests
    {
        private readonly Guid _reviewedSoftwareId = Guid.NewGuid();
        private readonly int _ratingMark = 5;
        private readonly Guid _ratedById = Guid.NewGuid();

        [Fact]
        public void Create_Should_Return_New_RatingSoftwareVersion()
        {
            // Act
            var rating = RatingSoftwareVersion.Create(_reviewedSoftwareId, _ratingMark, _ratedById);

            // Assert
            Assert.NotNull(rating);
            Assert.Equal(_reviewedSoftwareId, rating.SoftwareVersionId);
        }
    }
}
