using ITventory.Application.Services.RoomService.Add_iventory;
using ITventory.Application.Services.RoomService.Add_room;
using ITventory.Application.Services.RoomService.Reduce_inventory;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries;
using ITventory.Infrastructure.EF.Queries.Room;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers.Room
{
    public class roomController : BaseController
    {
        public roomController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddRoom command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpGet]
        public async Task<ICollection<RoomDTO>> Get([FromQuery] GetRoom query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }

        [HttpGet("{id:guid}")]
        public async Task<RoomDTO> GetById([FromRoute] Guid id)
        {
            var query = new GetRoomById { Id = id };
            return await _queryDispatcher.QueryAsync(query);
        }



        [HttpPut("inventory-add")]
        public async Task<IActionResult> Put([FromBody] AddInventory command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [HttpPut("inventory-reduce")]
        public async Task<IActionResult> Put([FromBody] ReduceInventory command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [HttpGet("inventory")]
        public async Task<ICollection<ProductInventoryDTO>> Get([FromQuery] GetInventoryProduct query)
        {
            return await _queryDispatcher.QueryAsync(query);
        }
    }
}
