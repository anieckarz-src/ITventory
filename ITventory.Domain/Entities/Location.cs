using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Location: Entity
    {
        public Guid Id { get; init; }
        public Guid CountryId { get; init; }
        public string Name { get; private set; }
        public ZipCode ZipCode { get; init; }
        public string City { get; init; }
        public Latitude Latitude { get; init; }
        public Longitude Longitude { get; init; }
        public TypeOfPlant? TypeOfPlant { get;private set; }


        private Location()
        {

        }

        public Location(string name, Guid countryId, ZipCode zipCode, string city, Latitude latitude, Longitude longitude, TypeOfPlant typeOfPlant)
        {
            if (String.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                throw new ArgumentNullException("Name must be at least 3 characters");
            }

            if (String.IsNullOrWhiteSpace(city) || city.Length < 2)
            {
                throw new ArgumentNullException("City must be at least 2 characters");
            }

            if(!Enum.IsDefined(typeof(TypeOfPlant), typeOfPlant))
            {
                throw new ArgumentException("Invalid plant type");
            }

            Id = Guid.NewGuid();
            Name = name;
            CountryId = countryId;
            ZipCode = zipCode;
            City = city;
            Latitude = latitude;
            Longitude = longitude;
            TypeOfPlant = typeOfPlant;

        }


    }
}
