using Microsoft.AspNetCore.Mvc;
using TalentTrack.Application.Features.JobTitles.Commands;
using TalentTrack.Application.Features.JobTitles.Queries;
using TalentTrack_Api.Controllers;


namespace TalentTrack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobTitleController : ApiControllerBase
{
    [HttpGet(nameof(GetAllJobTitle))]
    public async Task<ActionResult> GetAllJobTitle()
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(new GetAllJobTitlesQuery()));
        }
        return BadRequest();
    }

    [HttpGet(nameof(GetJobTitleById))]
    public async Task<ActionResult> GetJobTitleById(int Id)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(new GetJobTitlesByIdQuery(Id)));
        }
        return BadRequest();
    }

    [HttpPost(nameof(AddOrEditJobTitle))]
    public async Task<ActionResult> AddOrEditJobTitle(AddOrEditJobTitlesCommand command)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(command));
        }
        return BadRequest();
    }

    [HttpPost(nameof(DeleteJobTitle))]
    public async Task<ActionResult> DeleteJobTitle(DeleteJobTitlesCommand command)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(command));
        }
        return BadRequest();
    }

    [HttpPost(nameof(GetAllJobTitleWithSearch))]
    public async Task<ActionResult> GetAllJobTitleWithSearch(GetJobTitlesByNamePaginationQuery command)
    {
        if (Mediator != null)
        {
            return Ok(await Mediator.Send(command));
        }
        return BadRequest();
    }
}
