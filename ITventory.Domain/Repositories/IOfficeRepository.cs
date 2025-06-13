using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IOfficeRepository
    {
        Task<Office> GetAsync(Guid officeId);
        Task AddAsync(Office office);
        Task UpdateAsync(Office office);
        Task DeleteAsync(Office office);

        //read service w repozytorium
        Task<bool> ExistsById(Guid officeId);
    }
}
