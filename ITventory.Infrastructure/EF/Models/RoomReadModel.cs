using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class RoomReadModel
    {
        public Guid Id { get; set; }
        public Guid OfficeId { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public double Area { get; set; }
        public int Capacity { get; set; }
        public Guid PersonResponsibleId { get; set; }

        //public virtual OfficeReadModel Office { get; set; }
        public virtual EmployeeReadModel PersonResponsible { get; set; }
    }
}
