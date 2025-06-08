using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IHardwareRepository
    {
        Task<Hardware> GetAsync(Guid hardwareId);
        Task<Hardware> GetBySerialNumberAsync(string serialNumber);
        Task AddAsync(Hardware hardware);
        Task UpdateAsync(Hardware hardware);
        Task DeleteAsync(Hardware hardware);

        //read service w repozytorium
        Task<bool> ExistsById(Guid hardwareId);
        Task<bool> ExistsBySerialNumber(string serialNumber);
    }
}
