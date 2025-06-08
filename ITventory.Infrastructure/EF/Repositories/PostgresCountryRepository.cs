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
    internal class PostgresCountryRepository: ICountryRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Country> _countries;

        public PostgresCountryRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _countries = _writeDbContext.Countries;
        }

        public async Task AddAsync(Country country)
        {
            _countries.Add(country);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Country country)
        {
            _countries.Remove(country);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid countryId)
        {
            return _countries.AnyAsync(x => x.Id == countryId);
        }

        public Task<bool> ExistsByName(string name)
        {
            return _countries.AnyAsync(x => x.Name == name);
        }

        public Task<Country> GetAsync(Guid countryId)
        {
            return _countries.FirstOrDefaultAsync(x => x.Id == countryId);

        }

        public Task<Country> GetByNameAsync(string name)
        {
            return _countries.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(Country country)
        {
            _countries.Update(country);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
