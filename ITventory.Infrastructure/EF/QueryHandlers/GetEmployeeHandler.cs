using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Config.Read;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers
{
    internal sealed class GetEmployeeHandler : IQueryHandler<GetEmployee, ICollection<EmployeeDTO>>
    {
        private readonly DbSet<EmployeeReadModel> _employees;

        public GetEmployeeHandler(ReadDbContext readDbContext)
        {
            _employees = readDbContext.Employee;
        }

        public async Task<ICollection<EmployeeDTO>> HandleAsync(GetEmployee query)
        {
            var dbQuery = _employees
                .Include(x => x.Manager)
                .Include(x => x.Department)
                .Include(x => x.Room)
                .AsNoTracking()
                .AsQueryable();


            if (!String.IsNullOrWhiteSpace(query.Username))
            {
                dbQuery = dbQuery.Where(x => x.Username == query.Username);
            }
            if (!String.IsNullOrWhiteSpace(query.PositionName))
            {
                dbQuery = dbQuery.Where(x => x.PositionName == query.PositionName);
            }
            if (!String.IsNullOrWhiteSpace(query.Area))
            {
                dbQuery = dbQuery.Where(x => x.Area == query.Area);
            }
            if(query.DepartmentId.HasValue && query.DepartmentId.Value != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.DepartmentId == query.DepartmentId);
            }

            if (query.RoomId.HasValue && query.RoomId.Value != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.DepartmentId == query.DepartmentId);
            }

            if (query.IsActive.HasValue)
            {
                dbQuery = dbQuery.Where(x => x.IsActive == query.IsActive);
            }

            return await dbQuery.Select(x => new EmployeeDTO
            {
                Id = x.Id,
                Username = x.Username,
                Name = x.Name,
                LastName = x.LastName,
                IsActive = x.IsActive,
                Area = x.Area,
                PositionName = x.PositionName,
                Seniority = x.Seniority,
                ManagerId = x.ManagerId,
                DepartmentId = x.DepartmentId,
                HireDate = x.HireDate,
                BirthDate = x.BirthDate,
                RoomId = x.RoomId,

                Manager = x.Manager == null ? null : new EmployeeBasicDto
                {
                    Id = x.Manager.Id,
                    FullName = $"{x.Manager.Name}, {x.Manager.LastName}",
                    Seniority = x.Manager.Seniority,
                    PositionName = x.Manager.PositionName
                },

                Department = x.Department == null ? null : new DepartmentDTO
                {
                    Id = x.Department.Id,
                    Name = x.Department.Name
                },

                Room = x.Room == null ? null : new RoomDTO
                {
                    Id = x.Room.Id,
                    OfficeId = x.Room.Id,
                    RoomName = x.Room.RoomName,
                    Floor = x.Room.Floor,
                    Area = x.Room.Area,
                    Capacity = x.Room.Capacity,
                    PersonResponsibleId = x.Room.PersonResponsibleId,

                    PersonResponsible = x.Room.PersonResponsible == null ? null : new EmployeeBasicDto
                    {
                        Id = x.Room.PersonResponsible.Id,
                        Seniority = x.Room.PersonResponsible.Seniority,
                        PositionName = x.Room.PersonResponsible.PositionName
                    }
                }
            }).ToListAsync();




        }
    }
}
