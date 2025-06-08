using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.Reduce_inventory
{
    public record ReduceInventory (Guid roomId, Guid productId, int sku) : ICommand_;
   
}
