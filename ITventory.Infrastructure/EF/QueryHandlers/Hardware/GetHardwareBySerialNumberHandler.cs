using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.DTO.Hardware;
using ITventory.Application.Queries.Hardware;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.QueryHandlers.Hardware
{
    internal class GetHardwareBySerialNumberHandler : IQueryHandler<GetHardwareBySerialNumber, HardwareDTO>
    {
        public Task<HardwareDTO> HandleAsync(GetHardwareBySerialNumber query)
        {
            throw new NotImplementedException();
        }
    }
}
