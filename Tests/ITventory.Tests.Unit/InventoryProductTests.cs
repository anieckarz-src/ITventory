using ITventory.Domain;
using ITventory.Domain.Enums;
using System;
using Xunit;

namespace ITventory.Tests.Unit
{
    public class InventoryProductTests
    {
        private readonly Guid _roomId = Guid.NewGuid();
        private readonly Product _product = new Product("Test Product", ProductType.Consumable, 100.00, 200);
        private readonly int _initialSku = 50;

        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateInventoryProduct()
        {
            // Act
            var inventoryProduct = new InventoryProduct(_roomId, _product, _initialSku);

            // Assert
            Assert.NotEqual(Guid.Empty, inventoryProduct.Id);
            Assert.Equal(_roomId, inventoryProduct.RoomId);
            Assert.Equal(_product.Id, inventoryProduct.ProductId);
            Assert.Equal(_product, inventoryProduct.Product);
            Assert.Equal(_initialSku, inventoryProduct.SKU);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(201)]
        public void Constructor_WithInvalidSku_ShouldThrowArgumentException(int invalidSku)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new InventoryProduct(_roomId, _product, invalidSku));
        }

        [Fact]
        public void Create_Should_Return_New_InventoryProduct()
        {
            // Act
            var inventoryProduct = InventoryProduct.Create(_roomId, _product, _initialSku);

            // Assert
            Assert.NotNull(inventoryProduct);
            Assert.Equal(_initialSku, inventoryProduct.SKU);
        }

        [Fact]
        public void AddSku_Should_Increase_SKU()
        {
            // Arrange
            var inventoryProduct = new InventoryProduct(_roomId, _product, _initialSku);
            var valueToAdd = 50;

            // Act
            inventoryProduct.AddSku(valueToAdd);

            // Assert
            Assert.Equal(_initialSku + valueToAdd, inventoryProduct.SKU);
        }

        [Fact]
        public void AddSku_That_Exceeds_MaxSKU_Should_Throw_ArgumentException()
        {
            // Arrange
            var inventoryProduct = new InventoryProduct(_roomId, _product, _initialSku);
            var valueToAdd = 151;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => inventoryProduct.AddSku(valueToAdd));
        }

        [Fact]
        public void AddSku_With_NonPositive_Value_Should_Throw_ArgumentException()
        {
            // Arrange
            var inventoryProduct = new InventoryProduct(_roomId, _product, _initialSku);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => inventoryProduct.AddSku(0));
            Assert.Throws<ArgumentException>(() => inventoryProduct.AddSku(-10));
        }

        [Fact]
        public void ReduceSku_Should_Decrease_SKU()
        {
            // Arrange
            var inventoryProduct = new InventoryProduct(_roomId, _product, _initialSku);
            var valueToReduce = 30;

            // Act
            inventoryProduct.ReduceSku(valueToReduce);

            // Assert
            Assert.Equal(_initialSku - valueToReduce, inventoryProduct.SKU);
        }

        [Fact]
        public void ReduceSku_Below_Zero_Should_Throw_ArgumentException()
        {
            // Arrange
            var inventoryProduct = new InventoryProduct(_roomId, _product, _initialSku);
            var valueToReduce = 60;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => inventoryProduct.ReduceSku(valueToReduce));
        }
    }
}
