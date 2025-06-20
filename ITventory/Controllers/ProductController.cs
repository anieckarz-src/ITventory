using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using ITventory.Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;
using ITventory.Infrastructure.EF.DTO;
using ITventory.Infrastructure.EF.Queries.Product;
using ITventory.Application.Services.ProductService.Add_product;

namespace ITventory.Controllers
{
    public class productController : BaseController
    {
        public productController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProductDTO>>> Get([FromQuery] GetProduct query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet]

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProduct command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return Created();
        }

        
    }
}
