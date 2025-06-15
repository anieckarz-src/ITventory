using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.ProductService.Add_product
{
    public sealed class AddProductHandler : ICommandHandler<AddProduct>
    {
        private readonly IProductRepository _productRepository;

        public AddProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleAsync(AddProduct command)
        {
            var (description, productType, maxSKU, nominalWorth) = command;

            if (await _productRepository.ExistsByDescription(description))
            {
                throw new ArgumentException("Element with this description already exists");
            }

            var product = Product.Create(description, productType, nominalWorth, maxSKU);

            await _productRepository.AddAsync(product);
        }
    }
}
