using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITventory.Infrastructure.EF.Models
{
    public class DepartmentReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ManagerId { get; set; }
    
        public virtual EmployeeReadModel Manager { get; set; }

    }
}
