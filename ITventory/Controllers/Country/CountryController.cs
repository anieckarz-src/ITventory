using ITventory.Application.Services.CountryService.Add_country;
using ITventory.Application.Services.CountryService.Add_regulations;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers.Country
{
    public class CountryController : BaseController
    {
        public CountryController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCountry command)
        {

            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpPost("regulations")]
        public async Task<IActionResult> Post([FromBody] SetRegulations command)
        {

            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }
    }
}
