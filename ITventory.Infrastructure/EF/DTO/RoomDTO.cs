using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class RoomDTO
    {
        public Guid Id { get; set; }
        public Guid OfficeId { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public double Area { get; set; }
        public int Capacity { get; set; }
        public Guid PersonResponsibleId { get; set; }

        //public virtual OfficeReadModel Office { get; set; }
        public virtual EmployeeBasicDto PersonResponsible { get; set; }
    }
}
