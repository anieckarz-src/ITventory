using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface ILocationRepository
    {
        Task<Location> GetAsync(Guid locationid);
        Task<Location> GetByNameAsync(string name);
        Task AddAsync(Location location);
        Task UpdateAsync(Location location);
        Task DeleteAsync(Location location);

        //read service w repozytorium
        Task<bool> ExistsById(Guid locationid);
        Task<bool> ExistsByName(string location);
    }
}
