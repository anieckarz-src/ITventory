using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.DTO.Hardware;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Application.Queries.Hardware
{
    public class GetHardwareBySerialNumber: IQuery<HardwareDTO>
    {
        public Guid Id { get; set; }
    }
}
