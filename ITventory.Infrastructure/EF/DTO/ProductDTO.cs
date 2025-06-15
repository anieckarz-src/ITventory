using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public int MaxSKU { get; set; }
        public double NominalWorth { get; set; }
    }
}
