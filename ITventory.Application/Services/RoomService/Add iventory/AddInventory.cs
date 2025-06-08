using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.Add_iventory
{
    public record AddInventory (Guid roomId, Guid productId, int sku): ICommand_;
    
}
