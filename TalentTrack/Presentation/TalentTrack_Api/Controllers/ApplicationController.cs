using Microsoft.AspNetCore.Mvc;
using TalentTrack.Application.Features.Applicants.Commands;
using TalentTrack.Application.Features.Applicants.Queries;
using TalentTrack_Api.Controllers;


namespace TalentTrack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationController : ApiControllerBase
{
    [HttpGet(nameof(GetAllApplication))]
    public async Task<ActionResult> GetAllApplication()
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(new GetAllApplicantQuery()));
        }
        return BadRequest();
    }

    [HttpGet(nameof(GetApplicationById))]
    public async Task<ActionResult> GetApplicationById(int Id)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(new GetApplicantByIdQuery(Id)));
        }
        return BadRequest();
    }

    [HttpPost(nameof(AddOrEditApplication))]
    public async Task<ActionResult> AddOrEditApplication(AddOrEditApplicantCommand command)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(command));
        }
        return BadRequest();
    }

    [HttpPost(nameof(DeleteApplication))]
    public async Task<ActionResult> DeleteApplication(DeleteApplicantCommand command)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(command));
        }
        return BadRequest();
    }

    [HttpPost(nameof(GetAllApplicationWithSearch))]
    public async Task<ActionResult> GetAllApplicationWithSearch(GetApplicantByNamePaginationQuery command)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(command));
        }
        return BadRequest();
    }
}
