using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Room;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Room
{
    internal sealed class GetRoomByIdHandler : IQueryHandler<GetRoomById, RoomDTO>
    {
        private readonly DbSet<RoomReadModel> _rooms;

        public GetRoomByIdHandler(ReadDbContext readDbContxet)
        {
            _rooms = readDbContxet.Rooms;
        }
        public async Task<RoomDTO> HandleAsync(GetRoomById query)
        {
            var dbQuery = _rooms.Include(x => x.PersonResponsible).AsNoTracking().AsQueryable();

            return await dbQuery.Select(x => new RoomDTO
            {
                Id = x.Id,
                Area = x.Area,
                Capacity = x.Capacity,
                Floor = x.Floor,
                OfficeId = x.OfficeId,
                PersonResponsibleId = x.PersonResponsibleId,
                RoomName = x.RoomName,
                PersonResponsible = new EmployeeBasicDto
                {
                    FullName = $"{x.PersonResponsible.Name}, {x.PersonResponsible.LastName}",
                    Id = x.PersonResponsible.Id,
                    PositionName = x.PersonResponsible.PositionName,
                    Seniority = x.PersonResponsible.Seniority
                }
            }).FirstOrDefaultAsync();
        }
    }
}
