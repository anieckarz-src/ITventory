using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries.Product;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers.Products
{
    internal sealed class GetProductHandler : IQueryHandler<GetProduct, ICollection<ProductDTO>>
    {
        private readonly DbSet<ProductReadModel> _products;

        public GetProductHandler(ReadDbContext readDbContext)
        {
            _products = readDbContext.Products;
        }

        public async Task<ICollection<ProductDTO>> HandleAsync(GetProduct query)
        {

            var dbQuery = _products.AsQueryable();

            if(query.Description != null)
            {
                dbQuery = dbQuery.Where(x =>
                Microsoft.EntityFrameworkCore.EF.Functions.ILike(x.Description, $"%{query.Description}%"));
            }

            return await dbQuery
                .Select(x => new ProductDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    ProductType = x.ProductType,
                    MaxSKU = x.MaxSKU,
                    NominalWorth = x.NominalWorth,
                }).AsNoTracking().ToListAsync();
        }
    }
}
