using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries
{
    public class GetInventoryProduct: IQuery<ICollection<ProductInventoryDTO>>
    {
        public Guid? Id { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? ProductId { get; set; }
        public int? SKU { get; set; }
    }
}
