using ITventory.Application.Queries.Country;
using ITventory.Application.Queries.Producent;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ITventory.Controllers.Producent
{
    public class ProducentController : BaseController
    {
        public ProducentController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProducentDTO>>> Get([FromQuery] GetProducent query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProducentDTO>> GetById([FromRoute] Guid id)
        {
            var query = new GetProducentById { Id = id };
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }
    }
}
