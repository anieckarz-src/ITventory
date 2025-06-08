using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid productId);
        Task<Product> GetByDescriptionAsync(string description);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);

        //read service w repozytorium
        Task<bool> ExistsById(Guid productId);
        Task<bool> ExistsByDescription(string description);
    }
}
