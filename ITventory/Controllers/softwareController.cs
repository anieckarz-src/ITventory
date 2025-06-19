using ITventory.Application.Services.HardwareService.ReviewService.Add_review;
using ITventory.Application.Services.SoftwareService.Add_software;
using ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_rating;
using ITventory.Application.Services.SoftwareService.SoftwareVersionService.Add_version;
using ITventory.Application.Services.SoftwareService.SoftwareVersionService.Set_default_version;
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

        [HttpPut("version-default")]
        public async Task<IActionResult> Put([FromBody] SetDefaultVersion command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
    }
}
