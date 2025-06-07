using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.CountryService.Add_country
{
    public record CreateCountry(string name, string? countryCode, Region region): ICommand_;
}
