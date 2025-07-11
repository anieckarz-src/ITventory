﻿using ITventory.Application.Services.EmployeeService.ChangeManager;
using ITventory.Application.Services.EmployeeService.SetEmployeeDetails;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Employee;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers;

public class employeeController : BaseController
{
    public employeeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(
        commandDispatcher, queryDispatcher)
    {
    }

    [HttpPut("{employeeId:guid}")]
    public async Task<IActionResult> Put([FromBody] SetEmployeeDetails command, [FromRoute] Guid employeeId)
    {
        await _commandDispatcher.DispatchAsync(command with { userId = employeeId });
        return NoContent();
    }

    [HttpPut("manager")]
    public async Task<IActionResult> Put([FromBody] ChangeManager command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<EmployeeDTO> GetById([FromRoute] Guid id)
    {
        var query = new GetEmployeeById { Id = id };
        var resp = await _queryDispatcher.QueryAsync(query);

        return resp;
    }


    [HttpGet]
    public async Task<ICollection<EmployeeDTO>> Get([FromQuery] GetEmployee query)
    {
        return await _queryDispatcher.QueryAsync(query);
    }
}