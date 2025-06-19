using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.Queries.Producent;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.DTOProjections;
using ITventory.Infrastructure.EF.Models;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Producent
{
    internal sealed class GetProducentHandler : IQueryHandler<GetProducent, ICollection<ProducentDTO>>
    {
        private readonly DbSet<ProducentReadModel> _producents;

        public GetProducentHandler (ReadDbContext readDbContext)
        {
            _producents = readDbContext.Producents;
        }

        public async Task<ICollection<ProducentDTO>> HandleAsync(GetProducent query)
        {
            var dbQuery = _producents
                .Include(x => x.Country)
                .Include(x => x.Models)
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                dbQuery = dbQuery.Where(x =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(x.Name, $"%{query.Name}%"));
            }
            if(query.CountryId.HasValue && query.CountryId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.CountryId == query.CountryId);
            }

            return await dbQuery
                .Include(x => x.Country)
                .Include(x => x.Models)
                .Select(c => new ProducentDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryName = c.Country.Name,
                    Models = c.Models.Select(m => new ModelDTO
                    {
                        Id = m.Id,
                        Name = m.Name,
                        ReleaseDate = m.ReleaseDate,
                        Comments = m.Comments
                    }).ToList()

                }).ToListAsync();
        }
    }
}
