using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.CountryService.Add_regulations
{
    internal sealed class AddRegulationsHandler : ICommandHandler<SetRegulations>
    {
        private readonly ICountryRepository _countryRepository;

        public AddRegulationsHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }


        public async Task HandleAsync(SetRegulations command)
        {
            var (countryId, regulations) = command;
            
            var country = await _countryRepository.GetAsync(countryId);

            if (country is null)
            {
                throw new ArgumentException("Country not found");
            }

            country.SetRegulations(regulations);

            await _countryRepository.UpdateAsync(country);


        }
    }
}
