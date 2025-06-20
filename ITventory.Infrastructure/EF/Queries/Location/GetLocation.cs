using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries.Location
{
    public class GetLocation: IQuery<ICollection<LocationDTO>>
    {
        public string? Name { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? TypeOfPlant { get; set; }
    }
}
