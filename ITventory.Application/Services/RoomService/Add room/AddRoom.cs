using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.Add_room
{
    public record AddRoom (Guid OfficeId, int floor, double area, int capacity, Guid personResponsible, string name): ICommand_;
    
}
