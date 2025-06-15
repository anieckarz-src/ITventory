using ITventory.Application.Services.HardwareService.Add_hardware;
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
    }
}
