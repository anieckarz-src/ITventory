using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.Add_logon
{
    public record AddLogon (Guid hardwareId, Guid userId, Region domain, string ipAddress): ICommand_;
  
}
