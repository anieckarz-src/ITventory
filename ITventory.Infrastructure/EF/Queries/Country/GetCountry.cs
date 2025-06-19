using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.EF.Queries.Country
{
    public class GetCountry: IQuery<ICollection<CountryDTO>>
    {
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? Region { get; set; }

    }
}
