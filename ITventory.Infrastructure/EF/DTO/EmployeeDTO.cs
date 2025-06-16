using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }
        public string? Area { get; set; }
        public string? PositionName { get; set; }
        public string? Seniority { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? DepartmentId { get; set; }
        public DateOnly? HireDate { get; set; }
        public DateOnly? BirthDate { get; set; }
        public Guid? RoomId { get; set; }

        public EmployeeBasicDto? Manager { get; set; }
        public DepartmentDTO? Department { get; set; }
        public RoomDTO? Room { get; set; }
    }
}
