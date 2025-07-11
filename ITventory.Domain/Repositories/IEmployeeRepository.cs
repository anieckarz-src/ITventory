﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(Guid employeeId);
        Task<Employee> GetByUsernameAsync(string username);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);

        //read service w repozytorium
        Task<bool> ExistsById(Guid employeeId);
        Task<bool> ExistsByUsername(string username);
    }
}
