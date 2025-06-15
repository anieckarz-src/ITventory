using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITventory.Infrastructure.EF.DTO
{
    public class LocationDTO
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TypeOfPlant { get; set; }

    }
}
