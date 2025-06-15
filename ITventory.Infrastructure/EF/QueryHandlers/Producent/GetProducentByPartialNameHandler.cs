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
    internal sealed class GetProducentByPartialNameHandler : IQueryHandler<GetProducentByPartialName, ICollection<ProducentDTO>>
    {
        private readonly DbSet<ProducentReadModel> _producents;

        public GetProducentByPartialNameHandler (ReadDbContext readDbContext)
        {
            _producents = readDbContext.Producents;
        }

        public async Task<ICollection<ProducentDTO>> HandleAsync(GetProducentByPartialName query)
        {
            var dbQuery = _producents
                .Include(x => x.Country)
                .Include(x => x.Models)
                .AsQueryable();

            if (query.Name is not null)
            {
                dbQuery = dbQuery.Where(x =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(x.Name, $"%{query.Name}%"));
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
                        ProducentName = m.Producent.Name,
                        ReleaseDate = m.ReleaseDate,
                        Comments = m.Comments
                    }).ToList()

                }).ToListAsync();
        }
    }
}
