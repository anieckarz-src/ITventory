using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.ProductService.Change_nominal_worth
{
    internal sealed class ChangeNominalWorthHandler : ICommandHandler<ChangeNominalWorth>
    {
        private readonly IProductRepository _productRepository;

        public ChangeNominalWorthHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleAsync(ChangeNominalWorth command)
        {
            var (productId, nominalWorth) = command;

            var product = await _productRepository.GetAsync(productId)
                ?? throw new InvalidOperationException("Product not found");

            product.ChangeNominalWorth(nominalWorth);

            await _productRepository.UpdateAsync(product);
            
        }
    }
}
