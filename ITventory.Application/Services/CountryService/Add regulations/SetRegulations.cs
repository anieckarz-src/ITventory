using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.CountryService.Add_regulations
{
    public record SetRegulations(Guid countryId, string regulations): ICommand_;
   
}
