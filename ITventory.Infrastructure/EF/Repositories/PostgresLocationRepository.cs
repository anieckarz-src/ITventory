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
    internal class PostgresLocationRepository : ILocationRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Location> _locations;

        public PostgresLocationRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _locations = _writeDbContext.Locations;
        }

        public async Task AddAsync(Location location)
        {
            _locations.Add(location);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Location location)
        {
            _locations.Remove(location);

            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid locationid)
        {
            return _locations.AnyAsync(l => l.Id == locationid);
        }

        public Task<bool> ExistsByName(string location)
        {
            return _locations.AnyAsync(l => l.Name == location);
        }

        public Task<Location> GetAsync(Guid locationid)
        {
            return _locations.FirstOrDefaultAsync(l => l.Id==locationid);
        }

        public Task<Location> GetByNameAsync(string name)
        {
            return _locations.FirstOrDefaultAsync(l => l.Name==name);
        }

        public async Task UpdateAsync(Location locationid)
        {
            _locations.Update(locationid);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
