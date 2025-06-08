using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.EmployeeService.ChangeManager
{
    public record ChangeManager(Guid employeeId, Guid managerId): ICommand_;
   
}
