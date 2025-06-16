using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Application.Queries.Hardware
{
    public class GetHardware: IQuery<ICollection<HardwareDTO>>
    {
        public string? SerialNumber { get; set; }
        public string? Domain { get; set; }
        public string? HardwareType { get; set; }

    }
}
