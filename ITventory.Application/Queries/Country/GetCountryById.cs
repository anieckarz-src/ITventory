using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Application.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Application.Queries.Country
{
    public class GetCountryById: IQuery<CountryDTO>
    {
        public Guid Id { get; set; }
    }
}
