using ITventory.Domain;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class ProducentTests
    {
        private readonly string _name = "Test Producent";
        private readonly Guid _countryId = Guid.NewGuid();

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateProducent()
        {
            // Act
            var producent = new Producent(_name, _countryId);

            // Assert
            Assert.NotEqual(Guid.Empty, producent.Id);
            Assert.Equal(_name, producent.Name);
            Assert.Equal(_countryId, producent.CountryId);
        }
    }
}
