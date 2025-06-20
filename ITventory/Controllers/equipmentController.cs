using ITventory.Application.Services.Equipment_service.Add_equipment;
using ITventory.Application.Services.HardwareService.ReviewService.Add_review;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Equipment;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    public class equipmentController : BaseController
    {
        public equipmentController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEquipment command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AddReview command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ICollection<EquipmentDTO>> Get([FromQuery] GetEquipment query)
        {
            return await _queryDispatcher.QueryAsync(query);
            
        }
        [HttpGet("{id:guid}")]
        
        public async Task<EquipmentDTO> Get([FromRoute] Guid id)
        {
            var query = new GetEquipmentById { Id = id };
            return await _queryDispatcher.QueryAsync(query);
        }
    }


}
