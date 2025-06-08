using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.SoftwareService.Add_software;

public record CreateSoftware (Guid publisherId, ApprovalType approvalType, string name, Guid softwareVersionId) : ICommand_;

