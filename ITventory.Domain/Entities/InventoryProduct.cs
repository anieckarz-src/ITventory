using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class InventoryProduct: Entity
    {
        public Guid Id { get; init; }
        public Guid RoomId { get; private set; }
        public Guid ProductId { get; private set; } //for entity
        public Product Product { get; init; }
        public int SKU { get; private set; }


        private InventoryProduct()
        {

        }

        private InventoryProduct(Guid roomId, Product product, int sku)
        {
            if(sku < 0 || sku > Product.MaxSKU)
            {
                //wstawic domain service 
                throw new ArgumentException("Invalid sku: it must be between 1 and 999");
            }

            Id = Guid.NewGuid();
            RoomId = roomId;
            Product = product;
            ProductId = product.Id;
            SKU = sku;
        }

        public static InventoryProduct Create(Guid roomId, Product product, int sku)
        {
            return new InventoryProduct(roomId, product, sku);
        }

        public void AddSku(int value)
        {
            if(value <= 0)
            {
                throw new ArgumentException("You must provide positive number when adding SKU");
            }

            if(SKU + value > Product.MaxSKU)
            {
                throw new ArgumentException($"Max SKU exceeded ({Product.MaxSKU.ToString()}");
            }

            SKU += value;
        }

        public void ReduceSku(int value)
        {
            if (SKU - value < 0)
            {
                throw new ArgumentException("SKU cannot go below 0");
            }

            SKU -= value;
        }

    }
    
}
