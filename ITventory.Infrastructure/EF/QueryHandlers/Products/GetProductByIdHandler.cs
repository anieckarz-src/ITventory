using System;
using System.Collections.Generic;
using System.Linq;
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
    internal sealed class GetProductByIdHandler : IQueryHandler<GetProductById, ProductDTO>
    {
        private readonly DbSet<ProductReadModel> _products;

        public GetProductByIdHandler(ReadDbContext readCbContext)
        {
            _products = readCbContext.Products;
        }
        public async Task<ProductDTO> HandleAsync(GetProductById query)
        {
            var dbQuery = _products
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.Id == query.Id);

            return await dbQuery
                .Select(x => new ProductDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    ProductType = x.ProductType,
                    MaxSKU = x.MaxSKU,
                    NominalWorth = x.NominalWorth,
                }).SingleOrDefaultAsync();
        }
    }
}
