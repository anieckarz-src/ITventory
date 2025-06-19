using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class ProductInventoryReadModel
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid ProductId { get; set; }
        public int SKU { get; set; }

        public virtual RoomReadModel Room { get; set; }
        public virtual ProductReadModel Product { get; set; }
    }
}
