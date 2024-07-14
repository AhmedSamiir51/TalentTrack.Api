using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TalentTrack_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator? mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
