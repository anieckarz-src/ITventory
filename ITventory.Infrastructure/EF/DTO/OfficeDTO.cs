using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.Models;

namespace ITventory.Infrastructure.EF.DTO
{
    public class OfficeDTO
    {
        public Guid Id { get; set; }
        public string Street { get; set; }

        public string BuildingNumber { get; set; }
        public string FullAddress { get; set; }
        public Guid LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }

        public LocationDTO? Location { get; set; }
    }
}
