using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Department;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Department
{
    internal sealed class GetDepartmentHandler : IQueryHandler<GetDepartment, ICollection<DepartmentDTO>>
    {
        private readonly DbSet<DepartmentReadModel> _departments;

        public GetDepartmentHandler(ReadDbContext _readDbContxet)
        {
            _departments = _readDbContxet.Department;
        }

        public async Task<ICollection<DepartmentDTO>> HandleAsync(GetDepartment query)
        {
            var dbQuery = _departments
            .Include(x => x.Manager)
            .AsNoTracking()
            .AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                dbQuery = dbQuery.Where(x =>
                    Microsoft.EntityFrameworkCore.EF.Functions.ILike(x.Name, $"%{query.Name}%"));
            }
            if (query.DepartmentId.HasValue && query.DepartmentId.Value != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.Id == query.DepartmentId.Value);
            }


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
                }).ToListAsync();
        }
    }
}
