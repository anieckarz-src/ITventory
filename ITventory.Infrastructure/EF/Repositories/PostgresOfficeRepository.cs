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
    internal class PostgresOfficeRepository : IOfficeRepository
    {
        private readonly DbSet<Office> _office;
        private readonly WriteDbContext _writeDbContext;
        
        public PostgresOfficeRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _office = _writeDbContext.Office;
        }

        public async Task AddAsync(Office office)
        {
            _office.Add(office);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Office office)
        {
            _office.Remove(office);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid officeId)
        {
            return _office.AnyAsync(x => x.Id == officeId);
        }

        public Task<Office> GetAsync(Guid officeId)
        {
            return _office.SingleOrDefaultAsync(x => x.Id == officeId);
        }


        public Task UpdateAsync(Office office)
        {
            throw new NotImplementedException();
        }
    }
}
