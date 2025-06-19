using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;

namespace ITventory.Infrastructure.EF.DTO
{
    public class ModelDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProducentId { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string? Comments { get; set; }

        public virtual ProducentMinimalDTO? Producent { get; set; }
    }
}
