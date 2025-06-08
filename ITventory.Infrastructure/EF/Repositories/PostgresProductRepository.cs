using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.EF.Repositories
{
    internal class PostgresProductRepository: IProductRepository
    {
        private readonly WriteDbContext _writeDbContext;
        private readonly DbSet<Product> _products;

        public PostgresProductRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            _products = _writeDbContext.Products;
        }

        public async Task AddAsync(Product product)
        {
           _products.Add(product);
            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _products.Remove(product);
            await _writeDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsByDescription(string description)
        {
            return _products.AnyAsync(x => x.Description == description);
        }

        public Task<bool> ExistsById(Guid productId)
        {
            return _products.AnyAsync(x => x.Id == productId);
        }

        public Task<Product> GetAsync(Guid productId)
        {
            return _products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public Task<Product> GetByDescriptionAsync(string description)
        {
            return _products.FirstOrDefaultAsync(x => x.Description == description);
        }

        public async Task UpdateAsync(Product product)
        {
            _products.Update(product);
            await _writeDbContext.SaveChangesAsync();
        }
    }
}
