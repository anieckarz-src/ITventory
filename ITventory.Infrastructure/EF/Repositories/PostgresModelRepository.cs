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
    internal class PostgresModelRepository : IModelRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Model> _model;

        public PostgresModelRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _model = (DbSet<Model>)_writeDbContext.Model;
        }

        public Task<Model> GetAsync(Guid modelId)
        {
            return _model.FirstOrDefaultAsync(x => x.Id == modelId);
        }
    }
}