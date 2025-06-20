using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class SoftwareDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PublisherId { get; set; }
        public string ApprovalType { get; set; }

        public ProducentMinimalDTO? Publisher { get; set; }
        public ICollection<SoftwareVersionDTO>? Versions { get; set; }
    }
}
