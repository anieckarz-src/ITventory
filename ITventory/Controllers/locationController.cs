﻿using ITventory.Application.Services.CountryService.Add_regulations;
using ITventory.Application.Services.LocationService.AddLocation;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Country;
using ITventory.Infrastructure.EF.Queries.Location;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class locationController : BaseController
    {
        public locationController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddLocation command)
        {

            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpGet]

        public async Task<ICollection<LocationDTO>> Get([FromQuery] GetLocation query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }

        [HttpGet("{id:guid}")]

        public async Task<LocationDTO> GetById([FromRoute] Guid id)
        {
            var query = new GetLocationById { Id = id };

            return await _queryDispatcher.QueryAsync(query);
        }
    }
}
