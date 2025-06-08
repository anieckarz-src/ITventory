using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.DepartmentService.Add_department
{
    internal sealed class CreateDepartmentHandler : ICommandHandler<CreateDepartment>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }

        public async Task HandleAsync(CreateDepartment command)
        {
            var (name, managerId) = command;
            
            if (await _departmentRepository.ExistsByName(name))
            {
                throw new Exception("This department already exists");
            }

            var department = Department.Create(name, managerId);

            await _departmentRepository.AddAsync(department);
        }
    }
}
