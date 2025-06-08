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
    }
}
