using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class ProductInventoryDTO
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid ProductId { get; set; }
        public int SKU { get; set; }

        public RoomMinimalDTO Room { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
