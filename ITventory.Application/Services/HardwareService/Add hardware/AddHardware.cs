using System;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.Add_hardware
{
    public record AddHardware(
        Guid primaryUserId,
        Region defaultDomain,
        HardwareType hardwareType,
        string description,
        double worth,
        Guid producentId,
        Guid modelId,
        int modelYear,
        string serialNumber,
        DateOnly purchasedDate,
        Guid roomId,
        Guid departmentId
    ) : ICommand_;
}
