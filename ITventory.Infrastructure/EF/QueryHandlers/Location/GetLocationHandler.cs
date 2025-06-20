using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Location;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Location
{
    internal sealed class GetLocationHandler : IQueryHandler<GetLocation, ICollection<LocationDTO>>
    {
        private readonly DbSet<LocationReadModel> _locations;

        public GetLocationHandler(ReadDbContext readDbContext)
        {
            _locations = readDbContext.Locations;
        }
        public async Task<ICollection<LocationDTO>> HandleAsync(GetLocation query)
        {
            var dbQuery =
                _locations
                .Include(x => x.Country)
                .AsNoTracking()
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                dbQuery = dbQuery.Where(x => x.Name == query.Name);
            }

            if (!String.IsNullOrWhiteSpace(query.ZipCode))
            {
                dbQuery = dbQuery.Where(x => x.ZipCode == query.ZipCode);
            }

            if (!String.IsNullOrWhiteSpace(query.City))
            {
                dbQuery = dbQuery.Where(x => x.City == query.City);
            }

            if (!String.IsNullOrWhiteSpace(query.TypeOfPlant))
            {
                dbQuery = dbQuery.Where(x => x.TypeOfPlant == query.TypeOfPlant);
            }

            return await dbQuery.Select(x => new LocationDTO
            {
                City = x.City,
                Id = x.Id,
                CountryId = x.CountryId,
                CountryName = x.Country.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                ZipCode = x.ZipCode,
                Name = x.Name,
                TypeOfPlant = x.TypeOfPlant

            }).ToListAsync();
        }
    }
}
