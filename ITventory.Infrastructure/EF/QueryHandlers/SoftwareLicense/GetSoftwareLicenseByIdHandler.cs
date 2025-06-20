using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.SoftwareLicense;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.SoftwareLicense
{
    internal sealed class GetSoftwareLicenseByIdHandler : IQueryHandler<GetSoftwareLicenseById, SoftwareLicenseDTO>
    {
        private readonly DbSet<SoftwareLicenseReadModel> _softwareLicense;

        public GetSoftwareLicenseByIdHandler(ReadDbContext readDbContext)
        {
            _softwareLicense = readDbContext.SoftwareLicense;
        }
        public async Task<SoftwareLicenseDTO> HandleAsync(GetSoftwareLicenseById query)
        {
            var dbQuery = _softwareLicense
                .Include(x => x.Software)
                .AsNoTracking()
                .AsQueryable();

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
            }).FirstOrDefaultAsync();
        }


    }
}
