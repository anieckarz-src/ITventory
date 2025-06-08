using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Assign_hardware_to_license
{
    public record AssignHardwareToLicense(Guid licenseId, Guid hardwareId): ICommand_;
   
}
