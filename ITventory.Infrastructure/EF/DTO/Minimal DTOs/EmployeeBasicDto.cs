using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO
{
    public class EmployeeBasicDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Seniority { get; set; }
        public string? PositionName { get; set; }
    }
    
}
