using ITventory.Application.Services.HardwareService.ReviewService.Add_review;
using ITventory.Application.Services.SoftwareService.Add_software;
using ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_rating;
using ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_version;
using ITventory.Application.Services.SoftwareService.SoftwareVersionService.Set_default_version;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Software;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class softwareController : BaseController
    {
        public softwareController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSoftware command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpGet]
        
        public async Task<ICollection<SoftwareDTO>> Get([FromQuery] GetSoftware query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }

        [HttpGet("{id:guid}")]

        public async Task<SoftwareDTO> GetById([FromRoute] Guid id)
        {
            var query = new GetSoftwareById { Id = id };
            return await _queryDispatcher.QueryAsync(query);
        }


        [HttpPut("version")]
        public async Task<IActionResult> Put([FromBody] AddVersion command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpPut("review")]
        public async Task<IActionResult> Put([FromBody] AddSoftwareRating command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpPut("version/set-default")]
        public async Task<IActionResult> Put([FromBody] SetDefaultVersion command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
    }
}
