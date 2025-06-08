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
    internal class PostgresEquipmentRepository: IEquipmentRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Equipment> _equipment;

        public PostgresEquipmentRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _equipment = _writeDbContext.Equipment;
        }

        public async Task AddAsync(Equipment equipment)
        {
            _equipment.Add(equipment);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Equipment equipment)
        {
            _equipment.Remove(equipment);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsByDescription(string description)
        {
            return _equipment.AnyAsync(x => x.Description == description);
        }

        public Task<bool> ExistsById(Guid equipmentId)
        {
            return _equipment.AnyAsync(x => x.Id == equipmentId);
        }

        public Task<Equipment> GetAsync(Guid equipmentId)
        {
            return _equipment.FirstOrDefaultAsync(x => x.Id == equipmentId);
        }

        public Task<Equipment> GetByDescription(string description)
        {
            return _equipment.FirstOrDefaultAsync(x => x.Description == description);
        }

        public async Task UpdateAsync(Equipment equipment)
        {
            _equipment.Update(equipment);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
