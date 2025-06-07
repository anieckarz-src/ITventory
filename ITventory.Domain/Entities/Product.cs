using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.AbstractClasses;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Product: Entity
    {
        public Guid Id { get; init; }
        public string Description { get;init; }
        public ProductType ProductType { get; private set; }
        public int MaxSKU { get; private set; }
        public double NominalWorth { get; private set; }
        
        public Product(string descrption, ProductType productType, double nominalWorth, int maxsku=100)
        {
            if (String.IsNullOrWhiteSpace(descrption))
            {
                throw new ArgumentNullException("Description cannot be empty");
            }
            if(!Enum.IsDefined(typeof(ProductType), descrption))
            {
                throw new ArgumentException("Invalid product type");
            }
            if(maxsku < 1 || maxsku > 999)
            {
                throw new ArgumentException("Max SKU must be between 1 and 999");
            }
            if(nominalWorth <= 0)
            {
                throw new ArgumentException("Invalid nominal worth");
            }

            Id = Guid.NewGuid();
            Description = descrption;
            ProductType = productType;
            MaxSKU = maxsku;
            NominalWorth = nominalWorth;
        }

        public static Product Create(string description, ProductType productType, double nominalWorth, int maxSku)
        {
            return new Product(description, productType, nominalWorth, maxSku);
        }

        private Product()
        {
        }

        public void SetMaxSKU(int maxsku)
        {
            if(maxsku <= 0)
            {
                throw new ArgumentException("Max SKU cannot be negative");
            }

            MaxSKU = maxsku;
        }

        public void ChangeNominalWorth(double nominalWorth)
        {
            if(nominalWorth <= 0)
            {
                throw new ArgumentException("Invalid nominal worth");
            }

            NominalWorth = nominalWorth;
        }
    }
}
