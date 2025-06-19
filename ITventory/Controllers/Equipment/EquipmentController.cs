using ITventory.Application.Services.Equipment_service.Add_equipment;
using ITventory.Application.Services.HardwareService.ReviewService.Add_review;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries;
using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers.Equipment
{
    public class EquipmentController : BaseController
    {
        public EquipmentController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
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
    }


}
