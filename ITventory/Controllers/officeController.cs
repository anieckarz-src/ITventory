using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Office;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class officeController : BaseController
    {
        public officeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]
        public async Task<ICollection<OfficeDTO>> Get([FromQuery] GetOffice query)
        {
            return await _queryDispatcher.QueryAsync(query);
            
        }

        [HttpGet("{id:guid}")]
        public async Task<OfficeDTO> GetById([FromRoute] Guid id)
        {
            var query = new GetOfficeById { Id = id };
            return await _queryDispatcher.QueryAsync(query);

        }
    }
}
