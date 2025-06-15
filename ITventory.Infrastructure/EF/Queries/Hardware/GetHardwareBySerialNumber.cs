using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Application.Queries.Hardware
{
    public class GetHardwareBySerialNumber: IQuery<HardwareDTO>
    {
        public string SerialNumber { get; set; }
    }
}
