using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO.Minimal_DTOs
{
    public class ProducentMinimalDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? CountryName { get; set; }

    }
}
