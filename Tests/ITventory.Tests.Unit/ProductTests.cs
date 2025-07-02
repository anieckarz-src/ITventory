using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class ProductTests
    { 
        private readonly string _description = "Test Product";
        private readonly ProductType _productType = ProductType.Consumable;
        private readonly double _nominalWorth = 100.00;
        private readonly int _maxSku = 200;

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateProduct()
        {
            // Act
            var product = new Product(_description, _productType, _nominalWorth, _maxSku);

            // Assert
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal(_description, product.Description);
            Assert.Equal(_productType, product.ProductType);
            Assert.Equal(_nominalWorth, product.NominalWorth);
            Assert.Equal(_maxSku, product.MaxSKU);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_WithInvalidDescription_ShouldThrowArgumentNullException(string invalidDescription)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Product(invalidDescription, _productType, _nominalWorth, _maxSku));
        }

        [Fact]
        public void Constructor_WithInvalidProductType_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidProductType = (ProductType)999;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(_description, invalidProductType, _nominalWorth, _maxSku));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        public void Constructor_WithInvalidMaxSku_ShouldThrowArgumentException(int invalidMaxSku)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(_description, _productType, _nominalWorth, invalidMaxSku));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Constructor_WithInvalidNominalWorth_ShouldThrowArgumentException(double invalidNominalWorth)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(_description, _productType, invalidNominalWorth, _maxSku));
        }

        [Fact]
        public void Create_Should_Return_New_Product()
        {
            // Act
            var product = Product.Create(_description, _productType, _nominalWorth, _maxSku);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(_description, product.Description);
        }

        [Fact]
        public void SetMaxSKU_Should_Update_MaxSKU()
        {
            // Arrange
            var product = new Product(_description, _productType, _nominalWorth, _maxSku);
            var newMaxSku = 300;

            // Act
            product.SetMaxSKU(newMaxSku);

            // Assert
            Assert.Equal(newMaxSku, product.MaxSKU);
        }

        [Fact]
        public void SetMaxSKU_With_Invalid_Value_Should_Throw_ArgumentException()
        {
            // Arrange
            var product = new Product(_description, _productType, _nominalWorth, _maxSku);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.SetMaxSKU(0));
        }

        [Fact]
        public void ChangeNominalWorth_Should_Update_NominalWorth()
        {
            // Arrange
            var product = new Product(_description, _productType, _nominalWorth, _maxSku);
            var newNominalWorth = 150.00;

            // Act
            product.ChangeNominalWorth(newNominalWorth);

            // Assert
            Assert.Equal(newNominalWorth, product.NominalWorth);
        }

        [Fact]
        public void ChangeNominalWorth_With_Invalid_Value_Should_Throw_ArgumentException()
        {
            // Arrange
            var product = new Product(_description, _productType, _nominalWorth, _maxSku);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.ChangeNominalWorth(0));
        }
    }
}
