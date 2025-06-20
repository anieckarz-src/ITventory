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
    internal class GetLocationByIdHandler : IQueryHandler<GetLocationById, LocationDTO>
    {
        private readonly DbSet<LocationReadModel> _locations;

        public GetLocationByIdHandler(ReadDbContext readDbContext)
        {
            _locations = readDbContext.Locations;
        }
        public async Task<LocationDTO> HandleAsync(GetLocationById query)
        {
            var dbQuery =
               _locations
               .Include(x => x.Country)
               .AsNoTracking()
               .AsQueryable()
               .Where(x => x.Id == query.Id);

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

            }).FirstOrDefaultAsync();

        }
    }
}
