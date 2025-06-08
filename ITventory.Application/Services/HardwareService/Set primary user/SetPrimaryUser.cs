using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.Set_primary_user
{
    public record SetPrimaryUser (Guid hardwareId, Guid userId): ICommand_;
    
}
