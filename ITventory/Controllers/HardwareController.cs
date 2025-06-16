using ITventory.Application.Queries.Hardware;
using ITventory.Application.Services.HardwareService.Add_hardware;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class HardwareController : BaseController
    {
        public HardwareController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddHardware command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpGet]
        public async Task<ICollection<HardwareDTO>> Get([FromQuery] GetHardware query)
        {
            return await _queryDispatcher.QueryAsync(query);
            
        }
    }
}
