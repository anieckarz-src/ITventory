using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Country;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Country
{
    internal sealed class GetCountryHandler : IQueryHandler<GetCountry, ICollection<CountryDTO>>
    {
        private readonly DbSet<CountryReadModel> _countries;

        public GetCountryHandler(ReadDbContext readDbContext)
        {
            _countries = readDbContext.Countries;
        }

        public async Task<ICollection<CountryDTO>> HandleAsync(GetCountry query)
        {
            var dbQuery = _countries.Include(x => x.Locations).AsNoTracking().AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                dbQuery = dbQuery.Where(x => x.Name == query.Name);
            }
            if (!String.IsNullOrWhiteSpace(query.Region))
            {
                dbQuery = dbQuery.Where(x => x.Region == query.Region);
            }
            if (!String.IsNullOrWhiteSpace(query.CountryCode))
            {
                dbQuery = dbQuery.Where(x => x.CountryCode == query.CountryCode);
            }

            return await dbQuery
                 .Select(c => new CountryDTO
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Region = c.Region,
                     Regulations = c.Regulations,
                     CountryCode = c.CountryCode,
                     Locations = c.Locations.Select(l => new MinimalLocationDto
                     {
                         Name = l.Name,
                         City = l.City,
                         Latitude = l.Latitude,
                         Longitude = l.Longitude
                     }).ToList()
                 }
                 ).ToListAsync();
        }
    }
}
