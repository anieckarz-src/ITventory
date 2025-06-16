using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class EmployeeReadModel
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

        public virtual EmployeeReadModel Manager { get; set; }
        public virtual DepartmentReadModel Department {get;set;}
        public virtual RoomReadModel Room { get; set; }

    }
}
