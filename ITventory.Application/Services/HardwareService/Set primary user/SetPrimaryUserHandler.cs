using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.Set_primary_user
{
    public sealed class SetPrimaryUserHandler : ICommandHandler<SetPrimaryUser>
    {
        private readonly IHardwareRepository _hardwareRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public SetPrimaryUserHandler(IHardwareRepository hardwareRepository, IEmployeeRepository employeeRepository)
        {
            _hardwareRepository = hardwareRepository;
            _employeeRepository = employeeRepository;
        }



        public async Task HandleAsync(SetPrimaryUser command)
        {
            var (hardwareId, userId) = command;

            var hardware = await _hardwareRepository.GetAsync(hardwareId) ?? throw new InvalidOperationException("Hardware not found");
            var user = await _employeeRepository.GetAsync(userId) ?? throw new InvalidOperationException("User not found");

            hardware.SetPrimaryUser(user);
            await _hardwareRepository.UpdateAsync(hardware);
        }
    }
}
