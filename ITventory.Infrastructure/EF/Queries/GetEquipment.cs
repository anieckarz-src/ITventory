using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries
{
    public class GetEquipment : IQuery<ICollection<EquipmentDTO>>
    {
        public Guid? Id { get; set; }
        public string? Condition { get; set; }
        public string? Description { get; set; }
        public double? MinWorth { get; set; } = 0;
        public double? MaxWorht { get; set; } = 99999;
        public Guid? ModelId { get; set; }

    }
}
