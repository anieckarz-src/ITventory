using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.EmployeeService.ChangeManager
{
    internal sealed class ChangeManagerHandler : ICommandHandler<ChangeManager>
    {
        private readonly IEmployeeRepository _employeeRepository;


        public ChangeManagerHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task HandleAsync(ChangeManager command)
        {
            var (employeeId, managerId) = command;

            var user = await _employeeRepository.GetAsync(employeeId) ?? throw new InvalidOperationException("User not found");

            user.SetManager(managerId);
            await _employeeRepository.UpdateAsync(user);
            
        }
    }
}
