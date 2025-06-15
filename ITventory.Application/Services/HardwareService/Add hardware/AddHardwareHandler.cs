using System;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.Add_hardware
{
    public sealed class AddHardwareHandler : ICommandHandler<AddHardware>
    {
        private readonly IHardwareRepository _hardwareRepository;

        public AddHardwareHandler(IHardwareRepository hardwareRepository)
        {
            _hardwareRepository = hardwareRepository;
        }

        public async Task HandleAsync(AddHardware command)
        {
            var (
                primaryUserId,
                defaultDomain,
                hardwareType,
                description,
                worth,
                producentId,
                modelId,
                modelYear,
                serialNumber,
                purchasedDate,
                roomId,
                departmentId
            ) = command;

            var hardware = Hardware.Create(
                primaryUserId,
                defaultDomain,
                hardwareType,
                description,
                worth,
                producentId,
                modelId,
                modelYear,
                serialNumber,
                purchasedDate,
                roomId,
                departmentId
            );

            await _hardwareRepository.AddAsync(hardware);
        }
    }
}
