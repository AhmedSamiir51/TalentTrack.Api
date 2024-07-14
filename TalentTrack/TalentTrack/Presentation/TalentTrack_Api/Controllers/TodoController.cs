using Microsoft.AspNetCore.Mvc;
using TalentTrack.Application.Todos.Queries;

namespace TalentTrack_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ApiControllerBase
    {
        [HttpGet(nameof(GetTodo))]
        public async Task<ActionResult> GetTodo() => Ok(await Mediator.Send(new GetTodoQuery()));


    }
}
