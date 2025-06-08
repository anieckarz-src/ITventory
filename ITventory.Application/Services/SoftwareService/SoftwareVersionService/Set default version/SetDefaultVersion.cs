using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.SoftwareVersionService.Set_default_version
{
    public record SetDefaultVersion (Guid softwareId, Guid versionId): ICommand_;
   
}
