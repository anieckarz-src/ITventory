using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Room;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Room
{
    internal sealed class GetRoomHandler : IQueryHandler<GetRoom, ICollection<RoomDTO>>
    {
        private readonly DbSet<RoomReadModel> _rooms;

        public GetRoomHandler(ReadDbContext readDbContext)
        {
            _rooms = readDbContext.Rooms;
        }

        public async Task<ICollection<RoomDTO>> HandleAsync(GetRoom query)
        {
            var dbQuery = _rooms
                .Include(x => x.PersonResponsible)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.RoomName))
            {
                dbQuery = dbQuery.Where(x => x.RoomName == query.RoomName);
            }

            if (query.Floor.HasValue)
            {
                dbQuery = dbQuery.Where(x => x.Floor == query.Floor);
            }

            if (query.OfficeId.HasValue && query.OfficeId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.OfficeId == query.OfficeId);
            }

            if (query.PersonResponsibleId.HasValue && query.PersonResponsibleId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.PersonResponsibleId == query.PersonResponsibleId);
            }

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
            }).ToListAsync();
        }
    }
}
