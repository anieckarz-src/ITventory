using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Equipment;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Equipment
{
    internal sealed class GetEquipmentHandler : IQueryHandler<GetEquipment, ICollection<EquipmentDTO>>
    {
        private readonly DbSet<EquipmentReadModel> _equipment;

        public GetEquipmentHandler(ReadDbContext readCbContext)
        {
            _equipment = readCbContext.Equipment;
        }
        public async Task<ICollection<EquipmentDTO>> HandleAsync(GetEquipment query)
        {
            var dbQuery = _equipment
                .Include(x => x.Producent)
                .Include(x => x.Model)
                .Include(x => x.Room)
                .Include(x => x.Department)
                .AsNoTracking()
                .AsQueryable();

            if (query.Id.HasValue && query.Id != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.Id == query.Id);
            }

            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                dbQuery = dbQuery.Where (x => x.Description == query.Description);
            }

            if (!string.IsNullOrWhiteSpace(query.Condition))
            {
                dbQuery = dbQuery.Where(x => x.Condition == query.Condition);
            }

            if(query.MinWorth.HasValue && query.MaxWorht.HasValue)
            {
                dbQuery = dbQuery.Where(x => x.Worth > query.MinWorth.Value && x.Worth < query.MaxWorht);
            }
            if(query.ModelId.HasValue && query.ModelId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.ModelId == query.ModelId);
            }
            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                dbQuery = dbQuery.Where(x => x.Description == query.Description);
            }

            return await dbQuery.Select(x => new EquipmentDTO
            {
                Id = x.Id,
                Condition = x.Condition,
                Description = x.Description,
                Worth = x.Worth,
                ProducentId = x.ProducentId,
                ModelId = x.ModelId,
                ModelYear = x.ModelYear,
                SerialNumber = x.SerialNumber,
                PurchasedDate = x.PurchasedDate,
                RoomId = x.RoomId,
                DepartmentId = x.DepartmentId,

                Producent = x.Producent == null ? null : new ProducentMinimalDTO
                {
                    Id = x.Producent.Id,
                    Name = x.Producent.Name,
                    CountryName = x.Producent.Country.Name
                },

                Model = x.Model == null ? null : new ModelMinimalDTO
                {
                    Id = x.Model.Id,
                    Name = x.Model.Name
                },

                Room = x.Room == null ? null : new RoomMinimalDTO
                {
                    RoomName = x.Room.RoomName,
                    Floor = x.Room.Floor
                },

                Department = x.Department == null ? null : new DepartmentDTO
                {
                    Id = x.Department.Id,
                    Name = x.Department.Name
                }

            }).ToListAsync();
        }
    }
}
