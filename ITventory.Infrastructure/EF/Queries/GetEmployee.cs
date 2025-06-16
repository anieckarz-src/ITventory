using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries
{
    public class GetEmployee: IQuery<ICollection<EmployeeDTO>>
    {
        public string? Username { get; set; }
        public bool? IsActive { get; set; }
        public string? Area { get; set; }
        public string? PositionName { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? RoomId { get; set; }

    }
}
