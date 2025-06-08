using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IEquipmentRepository
    {
        Task<Equipment> GetAsync(Guid equipmentId);
        Task<Equipment> GetByDescription(string description);
        Task AddAsync(Equipment equipment);
        Task UpdateAsync(Equipment equipment);
        Task DeleteAsync(Equipment equipment);

        //read service w repozytorium
        Task<bool> ExistsById(Guid equipmentId);
        Task<bool> ExistsByDescription(string description);
    }
}
