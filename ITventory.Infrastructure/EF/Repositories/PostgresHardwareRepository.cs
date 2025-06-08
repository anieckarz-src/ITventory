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
    internal sealed class PostgresHardwareRepository: IHardwareRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Hardware> _hardware;

        public PostgresHardwareRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _hardware = _writeDbContext.Hardware;
        }

        public async Task AddAsync(Hardware hardware)
        {
            await _hardware.AddAsync(hardware);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Hardware hardware)
        {
            _hardware.Remove(hardware);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid hardwareId)
        {
            return _hardware.AnyAsync(x => x.Id == hardwareId);
        }

        public Task<bool> ExistsBySerialNumber(string serialNumber)
        {
            return _hardware.AnyAsync(x => x.SerialNumber == serialNumber);
        }

        public Task<Hardware> GetAsync(Guid hardwareId)
        {
            return _hardware.SingleOrDefaultAsync(x => x.Id == hardwareId);
        }

        public Task<Hardware> GetBySerialNumberAsync(string serialNumber)
        {
            return _hardware.SingleOrDefaultAsync(x => x.SerialNumber == serialNumber);
        }

        public async Task UpdateAsync(Hardware hardware)
        {
            _hardware.Update(hardware);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
