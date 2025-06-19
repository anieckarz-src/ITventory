using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.Queries.Country;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Country
{
    internal sealed class GetCountryByIdHandler : IQueryHandler<GetCountryById, CountryDTO>
    {
        private readonly DbSet<CountryReadModel> _countries;

        public GetCountryByIdHandler(ReadDbContext readDbContext)
        {
            _countries = readDbContext.Countries;
        }

        public async Task<CountryDTO> HandleAsync(GetCountryById query)
        {
            return await _countries
                .Include(l => l.Locations)
                .AsSplitQuery()
                .Where(c => c.Id == query.Id)
                .Select(c => new CountryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Region = c.Region,
                    Regulations = c.Regulations,
                    CountryCode = c.CountryCode,
                    Locations = c.Locations.Select(l =>

                        new MinimalLocationDto
                        {
                            Name = l.Name,
                            City = l.City,
                            Latitude = l.Latitude,
                            Longitude = l.Longitude

                        }
                    ).ToList()
   }
                ).FirstOrDefaultAsync();
        }
    }
}
