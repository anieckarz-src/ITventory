using ITventory.Domain;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class ModelTests
    {
        private readonly string _name = "Test Model";
        private readonly Guid _producentId = Guid.NewGuid();
        private readonly DateOnly _releaseDate = new DateOnly(2023, 1, 1);
        private readonly string _comments = "Test comments";

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateModel()
        {
            // Act
            var model = new Model(_name, _producentId, _releaseDate, _comments);

            // Assert
            Assert.NotEqual(Guid.Empty, model.Id);
            Assert.Equal(_name, model.Name);
            Assert.Equal(_producentId, model.ProducentId);
            Assert.Equal(_releaseDate, model.ReleaseDate);
            Assert.Equal(_comments, model.Comments);
        }

        [Fact]
        public void Create_Should_Return_New_Model()
        {
            // Act
            var model = Model.Create(_name, _producentId, _releaseDate, _comments);

            // Assert
            Assert.NotNull(model);
            Assert.Equal(_name, model.Name);
            Assert.Equal(_producentId, model.ProducentId);
        }
    }
}
