using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Domain;
using System.Xml.Linq;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.EmployeeService.SetEmployeeDetails
{
    internal sealed class SetEmployeeDetailsHandler : ICommandHandler<SetEmployeeDetails>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public SetEmployeeDetailsHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task HandleAsync(SetEmployeeDetails command)
        {
            var (userId, name, lastName, area, positionName,
            seniority, managerId, departmentId, hireDate,
            birthDate, roomId) = command;

            var employee = await _employeeRepository.GetAsync(userId);

            if(employee == null)
            {
                throw new InvalidOperationException("User not found");
            }

            employee.SetDetails(name, lastName, area, positionName, seniority, managerId, departmentId, hireDate, birthDate, roomId);
            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
