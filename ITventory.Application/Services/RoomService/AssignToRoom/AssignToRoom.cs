using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.RoomService.AssignToRoom
{
    public record AssignToRoom(Guid roomId, Employee employee): ICommand_;
    
}
