using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Software;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Software
{
    internal class GetSoftwareByIdQueryHandler : IQueryHandler<GetSoftwareById, SoftwareDTO>
    {
        private readonly DbSet<SoftwareReadModel> _software;

        public GetSoftwareByIdQueryHandler(ReadDbContext readDbContxet)
        {
            _software = readDbContxet.Software;
        }
        public async Task<SoftwareDTO> HandleAsync(GetSoftwareById query)
        {
            var dbQuery = _software.Include(x => x.Publisher)
                .Include(x => x.Versions)
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.Id == query.Id);


            return await dbQuery.Select(x => new SoftwareDTO
            {
                Id = x.Id,
                Name = x.Name,
                ApprovalType = x.ApprovalType,
                PublisherId = x.PublisherId,

                Publisher = new ProducentMinimalDTO
                {
                    Id = x.Publisher.Id,
                    Name = x.Publisher.Name,
                    CountryName = x.Publisher.Country.Name
                },

                Versions = x.Versions.Select(x => new SoftwareVersionDTO
                {
                    Id = x.Id,
                    VersionNumber = x.VersionNumber,
                    Published = x.Published,
                    SoftwareId = x.SoftwareId,
                    IsDefault = x.IsDefault,
                    IsActive = x.IsActive,
                    IsApproved = x.IsApproved,
                    LicenseType = x.LicenseType,
                    Price = x.Price,
                }).ToList()
            }).FirstOrDefaultAsync();
        }
    }
}
