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
    internal class PostgresRoomRepository: IRoomRepository
    {
        private readonly WriteDbContext _writeDbContxet;
        private readonly DbSet<Room> _rooms;

        public PostgresRoomRepository(WriteDbContext writeDbContxet)
        {
            _writeDbContxet = writeDbContxet;
            _rooms = _writeDbContxet.Rooms;
        }

        public async Task AddAsync(Room room)
        {
            _rooms.Add(room);
            await _writeDbContxet.SaveChangesAsync();
        }

        public async Task DeleteAsync(Room room)
        {
            _rooms.Remove(room);
            await _writeDbContxet.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid roomId)
        {
            return _rooms.AnyAsync(x => x.Id == roomId);
        }

        public Task<Room> GetAsync(Guid roomId)
        {
            return _rooms
                .Include(x => x.RoomInventory)
                .FirstOrDefaultAsync(x => x.Id == roomId);
        }

        public async Task UpdateAsync(Room room)
        {
            _rooms.Update(room);
            await _writeDbContxet.SaveChangesAsync();
        }

        public Task<bool> RoomExistsInOffice(Guid officeId, string name)
        {
            return _rooms.AnyAsync(x => x.OfficeId == officeId && x.RoomName == name);

        }
    }
}
