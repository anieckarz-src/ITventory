using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.CountryService.Add_country
{
    public sealed class CreateCountryHandler : ICommandHandler<CreateCountry>
    {
        private readonly ICountryRepository _countryRepository;

        public CreateCountryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }


        public async Task HandleAsync(CreateCountry command)
        {
            var (name, countryCode, region) = command;

            if (await _countryRepository.ExistsByName(name))
            {
                throw new Exception("Country with this name already exists");
            }

            var country = Country.Create(name, countryCode, region);

            _countryRepository.AddAsync(country);
            

            
        }
    }
}
