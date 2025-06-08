using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface ISoftwareRepository
    {
        Task<Software> GetAsync(Guid softwareId);
        Task<Software> GetByNameAsync(string name);
        Task AddAsync(Software software);
        Task UpdateAsync(Software software);
        Task DeleteAsync(Software software);

        //read service w repozytorium
        Task<bool> ExistsById(Guid softwareId);
        Task<bool> ExistsByName(string name);
    }
}
