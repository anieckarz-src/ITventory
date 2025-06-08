using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IProducentRepository
    {
        Task<Producent> GetAsync(Guid producentId);
        Task AddAsync(Producent producent);
        Task UpdateAsync(Producent producent);
        Task DeleteAsync(Producent producent);

        //read service w repozytorium
        Task<bool> ExistsById(Guid producentId);
    }
}
