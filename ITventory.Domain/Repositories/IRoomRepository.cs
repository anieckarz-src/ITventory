using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetAsync(Guid roomId);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(Room room);

        //read service w repozytorium
        Task<bool> ExistsById(Guid roomId);
    }
}
