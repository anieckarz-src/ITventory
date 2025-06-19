using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Model;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class modelController : BaseController
    {
        public modelController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]

        public async Task<ICollection<ModelDTO>> Get([FromQuery] GetModel query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }

        [HttpGet("{id:guid}")]
        
        public async Task<ModelDTO> GetById([FromRoute] Guid id)
        {
            var query = new GetModelById { Id = id };
            return await _queryDispatcher.QueryAsync(query);
        }
    }
}
