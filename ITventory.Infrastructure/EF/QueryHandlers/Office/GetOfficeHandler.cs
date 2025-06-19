using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Office;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Office
{
    internal sealed class GetOfficeHandler : IQueryHandler<GetOffice, ICollection<OfficeDTO>>
    {

        private readonly DbSet<OfficeReadModel> _offices;
        public GetOfficeHandler(ReadDbContext readDbContext)
        {
            _offices = readDbContext.Office;
        }

        public async Task<ICollection<OfficeDTO>> HandleAsync(GetOffice query)
        {
            var dbQuery = _offices.Include(x => x.Location).AsNoTracking().AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Street))
            {
                dbQuery = dbQuery.Where(x => x.Street == query.Street);
            }

            if (!String.IsNullOrWhiteSpace(query.BuildingNumber))
            {
                dbQuery = dbQuery.Where(x => x.BuildingNumber == query.BuildingNumber);
            }

            return await dbQuery.Select(x => new OfficeDTO
            {
                Id = x.Id,
                Street = x.Street,
                BuildingNumber = x.BuildingNumber,
                FullAddress = $"{x.Street}, Building number: {x.BuildingNumber}",
                IsActive = x.IsActive,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                LocationId = x.LocationId,

                Location = new LocationDTO
                {
                    Id = x.Location.Id,
                    City = x.Location.City,
                    CountryId = x.Location.CountryId,
                    CountryName = x.Location.Country.Name,
                    Latitude = x.Location.Latitude,
                    Longitude = x.Location.Longitude,
                    ZipCode = x.Location.ZipCode,
                    Name = x.Location.Name,
                    TypeOfPlant = x.Location.TypeOfPlant
                }
            }).ToListAsync();
        }
    }
}
