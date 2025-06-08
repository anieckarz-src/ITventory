using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Domain.Enums;

namespace ITventory.Application.Services.HardwareService.Add_hardware
{
    internal sealed class AddHardwareHandler : ICommandHandler<AddHardware>
    {
        private readonly IHardwareRepository _hardwareRepository;
        
        public AddHardwareHandler(IHardwareRepository hardwareRepository)
        {
            _hardwareRepository = hardwareRepository;
        }

        public async Task HandleAsync(AddHardware command)
        {
            var (primaryUserId, defaultDomain, hardwareType) = command;
            var newHardware = Hardware.Create(primaryUserId, defaultDomain, hardwareType);

            await _hardwareRepository.AddAsync(newHardware);
            
        }
    }
}
