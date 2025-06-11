using ITventory.Application.Services.CountryService.Add_regulations;
using ITventory.Application.Services.DepartmentService.Add_department;
using ITventory.Application.Services.LocationService.AddLocation;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers.Department
{
    public sealed class DepartmentController : BaseController
    {
        public DepartmentController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }


        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] CreateDepartment command)
        {

            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

    }
}
