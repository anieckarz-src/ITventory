using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;

namespace ITventory.Application.Services.EmployeeService.SetEmployeeDetails
{
    internal sealed class SetEmployeeDetails(string name, string lastName, Area area, string positionName,
                                             Seniority seniority, Guid managerId, Guid departmentId, DateOnly hireDate,
                                             DateOnly birthDate)
    {
    }
}
