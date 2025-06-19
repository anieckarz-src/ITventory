using ITventory.Application.Services.LicenseService.Add_license;
using ITventory.Application.Services.LicenseService.Assign_hardware_to_license;
using ITventory.Application.Services.LicenseService.Assign_user_to_license;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class licenseController : BaseController
    {
        public licenseController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLicense command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpPut("per-user")]
        public async Task<IActionResult> Put([FromBody] AssignUserToLicense command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("per-hardware")]

        public async Task<IActionResult> Put([FromBody] AssignHardwareToLicense command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }
    }
}
