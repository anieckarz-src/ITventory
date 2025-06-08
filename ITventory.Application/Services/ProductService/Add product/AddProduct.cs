using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.ProductService.Add_product
{
    public record AddProduct(string description, ProductType productType, int maxSKU, double nominalWorth): ICommand_;
    
}
