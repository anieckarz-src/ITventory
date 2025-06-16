using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.HardwareService.Add_logon
{
    public sealed class AddLogonHandler : ICommandHandler<AddLogon>
    {
        private readonly IHardwareRepository _hardwareRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AddLogonHandler(IHardwareRepository hardwareRepository, IEmployeeRepository employeeRepository)
        {
            _hardwareRepository = hardwareRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task HandleAsync(AddLogon command)
        {
            var (hardwareId, userId, domain, ipAddress) = command;

            var hardware = await _hardwareRepository.GetAsync(hardwareId) ?? throw new InvalidOperationException("Hardware not found");
            var user = await _employeeRepository.GetAsync(userId) ?? throw new InvalidOperationException("User not found");
            //Orkiestracja w warstwie aplikacji - sprawdzenie, czy obiekty do których chcemy się odwołać istnieją
            //Reguły biznesniwe -> warstwa domeny

            var logonTime = DateTime.UtcNow;

            var logon = Logon.Create(hardwareId, userId, domain, logonTime, ipAddress);
            hardware.AddLogon(logon);

            await _hardwareRepository.UpdateAsync(hardware);
        }
    }
}
