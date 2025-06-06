using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task<Country> GetAsync (Guid countryId);
        Task<Country> GetByNameAsync(string name);
        Task AddAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(Country country);
        

    }
}
