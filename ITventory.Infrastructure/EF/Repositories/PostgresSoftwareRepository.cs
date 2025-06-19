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
    internal class PostgresSoftwareRepository: ISoftwareRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Software> _software;

        public PostgresSoftwareRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _software = _writeDbContext.Software;
        }

        public async Task AddAsync(Software software)
        {
            _software.Add(software);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Software software)
        {
            _software.Remove(software);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid softwareId)
        {
            return _software.AnyAsync(x => x.Id == softwareId);
        }

        public Task<bool> ExistsByName(string name)
        {
            return _software.AnyAsync(x => x.Name == name);
        }

        public Task<Software> GetAsync(Guid softwareId)
        {
            return _software.Include(x => x.SoftwareVersions).FirstOrDefaultAsync(x => x.Id == softwareId);
        }

        public Task<Software> GetByNameAsync(string name)
        {
            return _software.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(Software software)
        {
            _software.Update(software);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
