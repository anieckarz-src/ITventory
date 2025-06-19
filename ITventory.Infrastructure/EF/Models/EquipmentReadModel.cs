using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.Models
{
    public class EquipmentReadModel
    {
        public Guid Id { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public double Worth { get; set; }
        public Guid ProducentId { get; set; }
        public Guid ModelId { get; set; }
        public int ModelYear { get; set; }
        public string SerialNumber { get; set; }
        public DateOnly PurchasedDate { get; set; }
        public Guid RoomId { get; set; }
        public Guid DepartmentId { get; set; }

        public virtual ProducentReadModel Producent { get; set; }
        public virtual ModelReadModel Model { get; set; }
        public virtual RoomReadModel Room { get; set; }
        public virtual DepartmentReadModel Department { get; set; } 

    }
}
