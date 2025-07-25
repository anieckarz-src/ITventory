﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;
using ITventory.Shared.Abstractions;

namespace ITventory.Domain
{
    public class Country
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? CountryCode {  get; set; }
        public Region Region { get; init; }
        public string? Regulations { get; private set; }

        public Country()
        {

        }

        public Country(string name, string? countryCode, Region region)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if(!Enum.IsDefined(typeof(Region), region))
            {
                throw new ArgumentOutOfRangeException(nameof(region));
            }

            this.Id = Guid.NewGuid();
            Name = name;
            CountryCode = countryCode;
            Region = region;
        }

        public static Country Create(string name, string? countryCode, Region region)
        {
            return new Country(name, countryCode, region);
        }

        public void SetRegulations(string regulations)
        {
            if (String.IsNullOrWhiteSpace(regulations))
            {
                throw new ArgumentNullException("Regulations cannot be empty");
            }

            this.Regulations = regulations;
        }
    }
}
