using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO.Minimal_DTOs
{
    public class HardwareMinimalDto
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public string HardwareType { get; set; }

    }
}
