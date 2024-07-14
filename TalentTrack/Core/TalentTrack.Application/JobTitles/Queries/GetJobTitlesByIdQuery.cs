using MediatR;
using TMG.SharedKernel.Common;
using TalentTrack.Application.Features.JobTitles.DTOS;

namespace TalentTrack.Application.Features.JobTitles.Queries;

public record GetJobTitlesByIdQuery(int Id) : IRequest<Result<JobTitleDto>>;

