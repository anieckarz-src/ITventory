using ITventory.Application.Queries.Hardware;
using ITventory.Application.Services.HardwareService.Add_hardware;
using ITventory.Application.Services.HardwareService.Add_logon;
using ITventory.Application.Services.HardwareService.Set_primary_user;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries;
using ITventory.Infrastructure.EF.Queries.Hardware;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class hardwareController : BaseController
    {
        public hardwareController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
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

        [HttpGet("{id:guid}")]
        public async Task<HardwareDTO> GetById([FromRoute] Guid id)
        {
            var query = new GetHardwareById { Id = id };
            return await _queryDispatcher.QueryAsync(query);
        }



        [HttpPost("logons")]
        public async Task<IActionResult> Post([FromBody] AddLogon command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpGet("logons")]

        public async Task<ICollection<LogonDTO>> Get([FromQuery] GetLogon query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }

        [HttpPut("primary-user")]
        public async Task<IActionResult> Put([FromBody] SetPrimaryUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

    }
}
