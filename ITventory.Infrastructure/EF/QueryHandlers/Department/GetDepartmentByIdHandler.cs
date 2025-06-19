using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Department;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Department
{
    

    internal sealed class GetDepartmentByIdHandler : IQueryHandler<GetDepartmentById, DepartmentDTO>
    {
        private readonly DbSet<DepartmentReadModel> _departments;

        public GetDepartmentByIdHandler(ReadDbContext readDbContext)
        {
            _departments = readDbContext.Department;
        }

        public async Task<DepartmentDTO> HandleAsync(GetDepartmentById query)
        {
            var dbQuery = _departments
            .Include(x => x.Manager)
            .AsNoTracking()
            .AsQueryable()
            .Where(x => x.Id == query.Id);


            return await dbQuery
                .Select(x => new DepartmentDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Manager = new EmployeeBasicDto
                    {
                        Id = x.Manager.Id,
                        FullName = $"{x.Manager.Name}, {x.Manager.LastName}",
                        PositionName = x.Manager.PositionName,
                        Seniority = x.Manager.Seniority
                    }
                }).FirstOrDefaultAsync();
        }
    }
}
