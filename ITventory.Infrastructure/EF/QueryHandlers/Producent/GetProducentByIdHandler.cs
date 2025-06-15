using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.Queries.Producent;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Producent
{
    internal class GetProducentByIdHandler : IQueryHandler<GetProducentById, ProducentDTO>
    {
        private readonly DbSet<ProducentReadModel> _producents;

        public GetProducentByIdHandler(ReadDbContext readDbContext)
        {
            _producents = readDbContext.Producents;
        }

        public async Task<ProducentDTO> HandleAsync(GetProducentById query)
        {
            return await _producents.Include(x => x.Country)
                .Include(x => x.Models)
                .Where(x => x.Id == query.Id)
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

                }).FirstOrDefaultAsync();
        }
    }
}
