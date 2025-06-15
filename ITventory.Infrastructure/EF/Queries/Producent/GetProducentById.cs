using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Application.Queries.Producent
{
    public class GetProducentById: IQuery<ProducentDTO>
    {
        public Guid Id { get; set; }
    }
}
