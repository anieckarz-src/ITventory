using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LocationService.AddLocation
{
    public sealed class AddLocationHandler : ICommandHandler<AddLocation>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILocationRepository _locationRepository;

        public AddLocationHandler(ICountryRepository countryRepository, ILocationRepository locationRepository)
        {
            _countryRepository = countryRepository;
            _locationRepository = locationRepository;
        }



        public async Task HandleAsync(AddLocation command)
        {
            var(name, countryId, zipCode, city, lattitude, longitude, typeOfPlant) = command;

            var country = _countryRepository.GetAsync(countryId) ?? throw new InvalidOperationException("Country not found");

            if (await _locationRepository.ExistsByName(name))
            {
                throw new InvalidOperationException("Location with this name already exists");
            }

            var location = Location.Create(name, countryId, zipCode, city, lattitude, longitude, typeOfPlant);
            await _locationRepository.AddAsync(location);

            

            
        }
    }
}
