using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.Repositories
{
    internal class PostgresEmployeeRepository: IEmployeeRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Employee> _employees;

        public PostgresEmployeeRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _employees = _writeDbContext.Employees;
        }

        public async Task AddAsync(Employee employee)
        {
            _employees.Add(employee);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _employees.Remove(employee);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsById(Guid employeeId)
        {
            return _employees.AnyAsync(x => x.Id == employeeId);
        }

        public Task<bool> ExistsByUsername(string username)
        {
            return _employees.AnyAsync(x => x.Username == username);
        }

        public Task<Employee> GetAsync(Guid employeeId)
        {
            return _employees.FirstOrDefaultAsync(x => x.Id == employeeId);
        }

        public Task<Employee> GetByUsernameAsync(string username)
        {
            return _employees.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _employees.Update(employee);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
