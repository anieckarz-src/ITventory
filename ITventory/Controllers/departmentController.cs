using ITventory.Application.Services.CountryService.Add_regulations;
using ITventory.Application.Services.DepartmentService.Add_department;
using ITventory.Application.Services.LocationService.AddLocation;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Department;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public sealed class departmentController : BaseController
    {
        public departmentController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }


        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] CreateDepartment command)
        {

            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] GetDepartment query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartmentsById([FromRoute] Guid id)
        {
            var query = new GetDepartmentById { Id = id };
            var result = await _queryDispatcher.QueryAsync(query);
            return Ok(result);
        }
    }
}
