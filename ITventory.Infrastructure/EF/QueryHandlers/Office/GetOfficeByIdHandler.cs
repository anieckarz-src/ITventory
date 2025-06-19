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
    internal sealed class GetOfficeByIdHandler : IQueryHandler<GetOfficeById, OfficeDTO>
    {
        private readonly DbSet<OfficeReadModel> _office;

        public GetOfficeByIdHandler(ReadDbContext readDbContext)
        {
            _office = readDbContext.Office;
        }
        public async Task<OfficeDTO> HandleAsync(GetOfficeById query)
        {
            var dbQuery = _office.Include(x => x.Location).AsNoTracking().AsQueryable();

            return await dbQuery.Where(x => x.Id == query.Id).Select(x => new OfficeDTO
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
            }).FirstOrDefaultAsync();
        
        }
    }
}
