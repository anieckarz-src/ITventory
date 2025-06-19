using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries.Room
{
    public class GetRoom: IQuery<ICollection<RoomDTO>>
    {
        public string? RoomName { get; set; }
        public Guid? OfficeId { get; set; }
        public int? Floor { get; set; }
        public Guid? PersonResponsibleId { get; set; }
    }
}
