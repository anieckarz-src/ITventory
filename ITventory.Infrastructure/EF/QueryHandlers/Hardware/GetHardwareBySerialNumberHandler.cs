using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.DTO;
using ITventory.Application.Queries.Hardware;
using ITventory.Infrastructure.EF.Models;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Hardware
{
    internal class GetHardwareBySerialNumberHandler : IQueryHandler<GetHardwareBySerialNumber, HardwareDTO>
    {
        private readonly DbSet<CountryReadModel> _countryReadModel;

        public Task<HardwareDTO> HandleAsync(GetHardwareBySerialNumber query)
        {
            throw new NotImplementedException();
        }
    }
}
