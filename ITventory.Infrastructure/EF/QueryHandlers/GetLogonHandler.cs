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
using ITventory.Infrastructure.EF.Queries;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers
{
    internal sealed class GetLogonHandler : IQueryHandler<GetLogon, ICollection<LogonDTO>>
    {
        private readonly DbSet<LogonReadModel> _logons;

        public GetLogonHandler(ReadDbContext readDbContext)
        {
            _logons = readDbContext.Logon;
        }

        public async Task<ICollection<LogonDTO>> HandleAsync(GetLogon query)
        {
            var dbQuery = _logons
                .Include(x => x.Hardware)
                .Include(x => x.User)
                .AsNoTracking()
                .AsQueryable();


            if (query.HardwareId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.HardwareId == query.HardwareId);
            }

            
            if (!String.IsNullOrWhiteSpace(query.IpAddress))
            {
                dbQuery = dbQuery.Where(x => x.IpAddress == query.IpAddress);
            }
            
            if (!String.IsNullOrWhiteSpace(query.Domain))
            {
                dbQuery = dbQuery.Where(x => x.Domain == query.Domain);
            }

           
            if(query.UserId.HasValue && query.UserId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.UserId == query.UserId);
            }

            return await dbQuery.Select(x => new LogonDTO
            {
                Id = x.Id,
                IpAddress = x.IpAddress,
                Domain = x.Domain,
                HardwareId = x.HardwareId,
                LogonTime = x.LogonTime,
                UserId = x.UserId,

                Hardware = x.Hardware == null ? null : new HardwareMinimalDto
                {
                    Id = x.Hardware.Id,
                    HardwareType = x.Hardware.HardwareType,
                    SerialNumber = x.Hardware.SerialNumber
                },

                User = x.User == null ? null : new EmployeeBasicDto
                {
                    Id = x.User.Id,
                    FullName = string.IsNullOrWhiteSpace(x.User.Name) && string.IsNullOrWhiteSpace(x.User.LastName)
                    ? "No info"
                    : $"{x.User.Name}, {x.User.LastName}",

                    PositionName = x.User.PositionName,
                    Seniority = x.User.Seniority
                }

            }).ToListAsync();
        }
    }
    
}
