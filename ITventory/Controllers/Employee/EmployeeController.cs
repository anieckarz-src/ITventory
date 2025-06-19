using ITventory.Application.Services.EmployeeService.ChangeManager;
using ITventory.Application.Services.EmployeeService.SetEmployeeDetails;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers.Employee
{
    public class EmployeeController : BaseController
    {
        public EmployeeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromBody] SetEmployeeDetails command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("manager")]
        public async Task<IActionResult> Put([FromBody] ChangeManager command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
            
        }
        


        [HttpGet]
        public async Task<ICollection<EmployeeDTO>> Get([FromQuery] GetEmployee query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }
    }
}
