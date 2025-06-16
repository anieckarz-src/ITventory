using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class HardwareDTO
    {

        public Guid Id { get; set; }
        public Guid PrimaryUserId { get; set; }
        public string DefaultDomain { get; set; }
        public string HardwareType { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public double Worth { get; set; }
        public Guid ProducentId { get; set; }
        public Guid ModelId { get; set; }
        public int ModelYear { get; set; }
        public string SerialNumber { get; set; }
        public DateOnly PurchasedDate { get; set; }
        public Guid RoomId { get; set; }
        public Guid DepartmentId { get; set; }

        public virtual ICollection<LogonDTO>? Logons { get; set; }
        public virtual EmployeeBasicDto PrimaryUser { get; set; }
        public virtual ProducentDTO Producent { get; set; }
        public virtual ModelDTO Model { get; set; }
        public virtual RoomDTO Room { get; set; }
        public virtual DepartmentDTO Department { get; set; }


    }

}
