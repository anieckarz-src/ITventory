using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Country> GetAsync(Guid departmentId);
        Task<Country> GetByNameAsync(string name);
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);

        //read service w repozytorium
        Task<bool> ExistsById(Guid departmentId);
        Task<bool> ExistsByName(string name);

    }
}
