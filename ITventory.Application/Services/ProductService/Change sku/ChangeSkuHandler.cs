using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.ProductService.Change_sku
{
    public sealed class ChangeSkuHandler : ICommandHandler<ChangeSku>
    {
        private readonly IProductRepository _productRepository;

        public ChangeSkuHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleAsync(ChangeSku command)
        {
            var(productId, sku) = command;

            var product = await _productRepository.GetAsync(productId);

            if(product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            product.SetMaxSKU(sku);
            await _productRepository.UpdateAsync(product);
        }
    }
}
