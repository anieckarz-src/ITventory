using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Employee;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Employee;

internal sealed class GetEmployeeByIdHandler : IQueryHandler<GetEmployeeById, EmployeeDTO>
{
    private readonly DbSet<EmployeeReadModel> _employees;

    public GetEmployeeByIdHandler(ReadDbContext readDbContext)
    {
        _employees = readDbContext.Employee;
    }

    public async Task<EmployeeDTO> HandleAsync(GetEmployeeById query)
    {
        var dbQuery = _employees.AsNoTracking().AsQueryable();

        return await dbQuery
            .Where(x => x.Id == query.Id)
            .Select(x => new EmployeeDTO
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

                Manager = x.Manager == null
                    ? null
                    : new EmployeeBasicDto
                    {
                        Id = x.Manager.Id,
                        FullName = $"{x.Manager.Name}, {x.Manager.LastName}",
                        Seniority = x.Manager.Seniority,
                        PositionName = x.Manager.PositionName
                    },

                Department = x.Department == null
                    ? null
                    : new DepartmentDTO
                    {
                        Id = x.Department.Id,
                        Name = x.Department.Name
                    },

                Room = x.Room == null
                    ? null
                    : new RoomDTO
                    {
                        Id = x.Room.Id,
                        OfficeId = x.Room.Id,
                        RoomName = x.Room.RoomName,
                        Floor = x.Room.Floor,
                        Area = x.Room.Area,
                        Capacity = x.Room.Capacity,
                        PersonResponsibleId = x.Room.PersonResponsibleId,

                        PersonResponsible = x.Room.PersonResponsible == null
                            ? null
                            : new EmployeeBasicDto
                            {
                                Id = x.Room.PersonResponsible.Id,
                                Seniority = x.Room.PersonResponsible.Seniority,
                                PositionName = x.Room.PersonResponsible.PositionName
                            }
                    }
            }).FirstOrDefaultAsync();
    }
}