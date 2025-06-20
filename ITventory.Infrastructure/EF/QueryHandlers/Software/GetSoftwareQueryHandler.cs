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
    internal sealed class GetSoftwareQueryHandler : IQueryHandler<GetSoftware, ICollection<SoftwareDTO>>
    {
        private readonly DbSet<SoftwareReadModel> _software;

        public GetSoftwareQueryHandler(ReadDbContext readDbContxet)
        {
            _software = readDbContxet.Software;
        }
        public async Task<ICollection<SoftwareDTO>> HandleAsync(GetSoftware query)
        {
            var dbQuery = _software.Include(x => x.Publisher)
                .Include(x => x.Versions)
                .AsNoTracking()
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                dbQuery = dbQuery.Where(x => x.Name == query.Name);
            }
            if(query.PublisherId.HasValue && query.PublisherId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.PublisherId == query.PublisherId);
            }
            if(!String.IsNullOrWhiteSpace(query.VersionId))
            {
                dbQuery = dbQuery.Where(x => x.Versions.Any(v => v.VersionNumber == query.VersionId));
            }

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
            }).ToListAsync();
        }
    }
}
