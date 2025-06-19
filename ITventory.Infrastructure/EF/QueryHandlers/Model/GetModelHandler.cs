using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Model;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Model
{
    internal sealed class GetModelHandler : IQueryHandler<GetModel, ICollection<ModelDTO>>
    {
        private readonly DbSet<ModelReadModel> _models;

        public GetModelHandler(ReadDbContext readDbContext)
        {
            _models = readDbContext.Models;
        }
        public async Task<ICollection<ModelDTO>> HandleAsync(GetModel query)
        {
            var dbQuery = _models.Include(x => x.Producent).AsNoTracking().AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                dbQuery = dbQuery.Where(x => x.Name == query.Name);
            }

            return await dbQuery.Select(x => new ModelDTO
            {
                Id = x.Id,
                ReleaseDate = x.ReleaseDate,
                Name = x.Name,
                Comments = x.Comments,
                ProducentId = x.ProducentId,
                Producent = new ProducentMinimalDTO
                {
                    CountryName = x.Producent.Country.Name,
                    Id = x.Producent.Id,
                    Name = x.Producent.Name,
                }
            }).ToListAsync();
        }
    }
}
