using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.Repositories
{
    internal class PostgresLicenseRepository: ILicenseRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<SoftwareLicense> _licenses;

        public PostgresLicenseRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _licenses = _writeDbContext.SoftwareLicense;
        }

        public async Task AddAsync(SoftwareLicense license)
        {
            _licenses.Add(license);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SoftwareLicense license)
        {
            _licenses.Remove(license);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid licenseId)
        {
            return _licenses.AnyAsync(x => x.Id == licenseId);
        }

        public Task<SoftwareLicense> GetAsync(Guid licenseId)
        {
            return _licenses
                .Include(x => x.AssignedUsers)
                .Include(x => x.AssignedHardware)
                
                .SingleOrDefaultAsync(x => x.Id == licenseId);
        }

        public async Task UpdateAsync(SoftwareLicense license)
        {
            _licenses.Update(license);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
