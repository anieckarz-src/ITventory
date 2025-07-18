﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Hardware;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Hardware
{
    internal sealed class GetHardwareByIdHandler : IQueryHandler<GetHardwareById, HardwareDTO>
    {
        private readonly DbSet<HardwareReadModel> _hardware;

        public GetHardwareByIdHandler(ReadDbContext readDbContext)
        {
            _hardware = readDbContext.Hardware;
        }

        public async Task<HardwareDTO> HandleAsync(GetHardwareById query)
        {
            var dbQuery = _hardware
                .Include(x => x.PrimaryUser)
                .Include(x => x.Producent)
                .Include(x => x.Model)
                .Include(x => x.Room)
                .Include(x => x.Department)
                .Where(x => x.Id == query.Id)
                .AsNoTracking()
                .AsQueryable();
          

            return await dbQuery.Select(x => new HardwareDTO
            {
                Id = x.Id,
                PrimaryUserId = x.PrimaryUserId,
                DefaultDomain = x.DefaultDomain,
                HardwareType = x.HardwareType,
                IsActive = x.IsActive,
                Description = x.Description,
                Worth = x.Worth,
                ProducentId = x.ProducentId,
                ModelId = x.ModelId,
                ModelYear = x.ModelYear,
                SerialNumber = x.SerialNumber,
                PurchasedDate = x.PurchasedDate,
                RoomId = x.RoomId,
                DepartmentId = x.DepartmentId,

                Logons = x.Logons.Select(x => new LogonDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Domain = x.Domain,
                    LogonTime = x.LogonTime,
                    IpAddress = x.IpAddress,
                    User = new EmployeeBasicDto
                    {
                        Id = x.User.Id,
                        FullName = $"{x.User.Name}, {x.User.LastName}",
                        Seniority = x.User.Seniority,
                        PositionName = x.User.PositionName,
                    }

                }).ToList() ?? new List<LogonDTO>(),

                PrimaryUser = new EmployeeBasicDto
                {
                    Id = x.PrimaryUser.Id,
                    FullName = $"{x.PrimaryUser.Name}, {x.PrimaryUser.LastName}",
                    PositionName = x.PrimaryUser.PositionName,
                    Seniority = x.PrimaryUser.Seniority
                },

                Producent = new ProducentMinimalDTO
                {
                    Id = x.Producent.Id,
                    Name = x.Producent.Name,
                    CountryName = x.Producent.Country.Name,
                },

                Model = new ModelDTO
                {
                    Id = x.Model.Id,
                    Name = x.Model.Name,
                    ReleaseDate = x.Model.ReleaseDate,
                    Comments = x.Model.Comments
                },

                Room = new RoomMinimalDTO
                {
                    RoomName = x.Room.RoomName,
                    Floor = x.Room.Floor

                },

                Department = new DepartmentDTO
                {
                    Name = x.Department.Name
                }

            }).FirstOrDefaultAsync();
        }
    }
}
