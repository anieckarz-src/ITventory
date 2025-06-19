using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using ITventory.Shared.Abstractions;

namespace ITventory.Infrastructure.EF.Models
{
    public class LocationReadModel
    {

        public Guid Id { get; init; }
        public Guid CountryId { get; init; }
        public string Name { get; private set; }
        public string ZipCode { get; init; }
        public string City { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string? TypeOfPlant { get; private set; }

        //

        public virtual CountryReadModel Country { get; set; }


    }
 }
