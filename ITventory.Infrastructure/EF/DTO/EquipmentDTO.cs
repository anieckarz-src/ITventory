using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class EquipmentDTO
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

        public virtual ProducentMinimalDTO Producent { get; set; }
        public virtual ModelMinimalDTO Model { get; set; }
        public virtual RoomMinimalDTO Room { get; set; }
        public virtual DepartmentDTO Department { get; set; }
    }
}
