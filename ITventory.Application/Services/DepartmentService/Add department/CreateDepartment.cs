using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.DepartmentService.Add_department
{
    public record CreateDepartment(string name, Guid managerId): ICommand_;
    
}
