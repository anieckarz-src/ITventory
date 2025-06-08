using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_version
{
    public record AddVersion(Guid softwareId, string versionNumber, decimal price, DateOnly published, LicenseType licenseType): ICommand_;
    
}
