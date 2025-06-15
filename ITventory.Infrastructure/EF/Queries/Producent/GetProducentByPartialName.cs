using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Application.Queries.Producent
{
    public class GetProducentByPartialName: IQuery<ICollection<ProducentDTO>>
    {
        public string Name { get; set; }
    }
}
