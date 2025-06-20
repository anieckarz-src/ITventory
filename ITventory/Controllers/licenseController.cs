using ITventory.Application.Services.LicenseService.Add_license;
using ITventory.Application.Services.LicenseService.Assign_hardware_to_license;
using ITventory.Application.Services.LicenseService.Assign_user_to_license;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.SoftwareLicense;
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

        [HttpGet]
        
        public async Task<ICollection<SoftwareLicenseDTO>> Get([FromQuery] GetSoftwareLicense query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }

        [HttpGet("{id:guid}")]

        public async Task<SoftwareLicenseDTO> GetById([FromRoute] Guid id)
        {
            var query = new GetSoftwareLicenseById { Id = id };
            return await _queryDispatcher.QueryAsync(query);
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
