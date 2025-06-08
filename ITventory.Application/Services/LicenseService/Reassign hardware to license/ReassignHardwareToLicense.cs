using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Reassign_hardware_to_license
{
    public record ReassignHardwareToLicense(Guid licenseId, Guid newHardwareId, Guid oldHardwareId): ICommand_;
    
}
