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
    internal class PostgresDepartmentRepository: IDepartmentRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Department> _departments;

        public PostgresDepartmentRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _departments = _writeDbContext.Departments;
        }

        public async Task AddAsync(Department department)
        {
            _departments.Add(department);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {
            _departments.Remove(department);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid departmentId)
        {
            return _departments.AnyAsync(x => x.Id == departmentId);
        }

        public Task<bool> ExistsByName(string name)
        {
            return _departments.AnyAsync(x => x.Name == name);
        }

        public Task<Department> GetAsync(Guid departmentId)
        {
            return _departments.SingleOrDefaultAsync(x => x.Id ==departmentId);
        }

        public Task<Department> GetByNameAsync(string name)
        {
            return _departments.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(Department department)
        {
            _departments.Update(department);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
