using ITventory.Application.Services.RoomService.Add_room;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers.Room
{
    public class RoomController : BaseController
    {
        public RoomController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] AddRoom command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }
    }
}
