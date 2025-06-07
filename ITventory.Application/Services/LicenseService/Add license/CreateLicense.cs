using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Add_license
{
    public record CreateLicense(LicenseType LicenseType, string LicenseKey, DateOnly ValidUntil, int MaxUse) : ICommand_
    {
    }
    
}
