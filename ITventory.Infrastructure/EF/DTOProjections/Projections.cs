using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTOProjections
{
    public static class Projections
    {
        public static ProducentDTO AsDto(this ProducentReadModel readModel) =>
            new()
            {
                Id = readModel.Id,
                Name = readModel.Name,
                CountryName = readModel.Country.Name,
                Models = readModel.Models?.Select(c => new ModelDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProducentName = c.Producent.Name,
                    ReleaseDate = c.ReleaseDate,
                    Comments = c.Comments
                }).ToList()

            };
        
    }
}
