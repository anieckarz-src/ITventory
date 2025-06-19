using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Contexts;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.DTO.Minimal_DTOs;
using ITventory.Infrastructure.EF.Models;
using ITventory.Infrastructure.EF.Queries;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.QueryHandlers
{
    internal sealed class GetProductInventoryHandler : IQueryHandler<GetInventoryProduct, ICollection<ProductInventoryDTO>>
    {
        private readonly DbSet<ProductInventoryReadModel> _productInventory;

        public GetProductInventoryHandler(ReadDbContext readDbContext)
        {
            _productInventory = readDbContext.ProductInventory;
        }

        public async Task<ICollection<ProductInventoryDTO>> HandleAsync(GetInventoryProduct query)
        {
            var dbQuery = _productInventory
                .Include(x => x.Product)
                .Include(x => x.Room)
                .AsNoTracking()
                .AsQueryable();

            if(query.Id.HasValue && query.Id != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.Id == query.Id);
            }

            if (query.ProductId.HasValue && query.ProductId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.ProductId == query.ProductId);
            }

            if (query.RoomId.HasValue && query.RoomId != Guid.Empty)
            {
                dbQuery = dbQuery.Where(x => x.RoomId == query.RoomId);
            }

            if (query.SKU.HasValue && query != null)
            {
                dbQuery = dbQuery.Where(x => x.SKU > query.SKU);
            }

            return await dbQuery.Select(x => new ProductInventoryDTO
            {
                Id = x.Id,
                ProductId = x.ProductId,
                RoomId = x.RoomId,
                SKU = x.SKU,

                Product = new ProductDTO
                {
                    Id = x.Product.Id,
                    Description = x.Product.Description,
                    MaxSKU = x.Product.MaxSKU,
                    NominalWorth = x.Product.NominalWorth,
                    ProductType = x.Product.ProductType,
                },

                Room = new RoomMinimalDTO
                {
                    RoomName = x.Room.RoomName,
                    Floor = x.Room.Floor,
                }

            }).ToListAsync();


        }
    }
}
