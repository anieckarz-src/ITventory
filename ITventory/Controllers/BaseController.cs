using ITventory.Shared.Abstractions.Commands;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ITventory.Controllers
{
    [ApiController]
    [Route("itventory/[controller]")]

    public abstract class BaseController : ControllerBase
    {
        protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
        {
            return result is null ? NotFound() : Ok(result);
        }


        protected readonly ICommandDispatcher _commandDispatcher;
        protected readonly IQueryDispatcher _queryDispatcher;

        public BaseController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
    }
}
