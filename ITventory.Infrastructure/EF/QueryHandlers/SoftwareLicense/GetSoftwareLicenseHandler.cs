using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.SoftwareLicense;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.SoftwareLicense
{
    internal sealed class GetSoftwareLicenseHandler : IQueryHandler<GetSoftwareLicense, ICollection<SoftwareLicenseDTO>>
    {
        private readonly DbSet<SoftwareLicenseReadModel> _sofwareLicense;

        public GetSoftwareLicenseHandler(ReadDbContext readDbContext)
        {
            _sofwareLicense = readDbContext.SoftwareLicense;
        }
        public async Task<ICollection<SoftwareLicenseDTO>> HandleAsync(GetSoftwareLicense query)
        {
            var dbQuery = _sofwareLicense.Include(x => x.Software).AsNoTracking().AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.LicenseKey))
            {
                dbQuery = dbQuery.Where(x => x.LicenseKey == query.LicenseKey);
            }
            if(query.SoftwareVersionId.HasValue && query.SoftwareVersionId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.SoftwareVersion == query.SoftwareVersionId);
            }
            if (query.MaxUse.HasValue)
            {
                dbQuery = dbQuery.Where(x => x.MaxUse < query.MaxUse);
                //Zwracamy wszystkie te, które są poniżej ustalonego limitu
            }

            if (query.MaxValidUntill.HasValue)
            {
                dbQuery = dbQuery.Where(x => x.ValidUntil <= query.MaxValidUntill);
            }

            return await dbQuery.Select(x => new SoftwareLicenseDTO
            {
                Id = x.Id,
                LicenseKey = x.LicenseKey,
                LicenseType = x.LicenseType,
                MaxUse = x.MaxUse,
                SoftwareVersion = x.SoftwareVersion,
                ValidUntil = x.ValidUntil,
                Software = new SoftwareVersionDTO
                {
                    Id = x.Software.Id,
                    LicenseType = x.Software.LicenseType,
                    Price = x.Software.Price,
                    Published = x.Software.Published,
                    VersionNumber = x.Software.VersionNumber,
                }
            }).ToListAsync();
        }
    }
}
