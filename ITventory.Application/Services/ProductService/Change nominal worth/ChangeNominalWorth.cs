using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.ProductService.Change_nominal_worth
{
    public record ChangeNominalWorth (Guid productId, double worth): ICommand_;
   
}
