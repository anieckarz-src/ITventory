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
    internal class PostgresProducentRepository: IProducentRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Producent> _producents;

        public PostgresProducentRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _producents = _writeDbContext.Producents;
        }

        public async Task AddAsync(Producent producent)
        {
            _producents.Add(producent);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Producent producent)
        {
            _producents.Remove(producent);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid producentId)
        {
            return _producents.AnyAsync(x => x.Id == producentId);
        }

        public Task<Producent> GetAsync(Guid producentId)
        {
            return _producents.FirstOrDefaultAsync(x => x.Id == producentId);
        }

        public async Task UpdateAsync(Producent producent)
        {
            _producents.Update(producent);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
