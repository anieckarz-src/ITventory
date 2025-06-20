using System;
using System.Collections.Generic;
using System.Linq;
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
    internal sealed class GetEquipmentByIdHandler : IQueryHandler<GetEquipmentById, EquipmentDTO>
    {
        private readonly DbSet<EquipmentReadModel> _equipment;

        public GetEquipmentByIdHandler(ReadDbContext readDbContext)
        {
            _equipment = readDbContext.Equipment;
        }
        public async Task<EquipmentDTO> HandleAsync(GetEquipmentById query)
        {
            var dbQuery = _equipment
                .Include(x => x.Producent)
                .Include(x => x.Room)
                .Include(x => x.Model)
                .Include(x => x.Department)
                .AsNoTracking()
                .AsQueryable();

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

            }).FirstOrDefaultAsync();
        }
    }
}
