using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.EmployeeService.SetEmployeeDetails
{
    public record SetEmployeeDetails(Guid userId, string name, string lastName, Area area, string positionName,
                                             Seniority seniority, Guid managerId, Guid departmentId, DateOnly hireDate,
                                             DateOnly birthDate, Guid roomId): ICommand_;
    
}
