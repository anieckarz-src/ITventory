using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LicenseService.Reassign_user_to_license
{
    public record ReassignUserToLicense (Guid licenseId, Guid newUserId, Guid oldUserId): ICommand_;
    
    
}
