using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.Add_hardware
{
    public record AddHardware(Guid primaryUserId, Region defaultDomain, HardwareType hardwareType): ICommand_;
 
}
