using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Domain.Enums;

namespace ITventory.Infrastructure.EF.Models
{
    public class CountryReadModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? CountryCode { get; set; }
        public string Region { get; init; }
        public string? Regulations { get; private set; }

        public virtual ICollection<LocationReadModel> Locations { get; set; }
    }
}
