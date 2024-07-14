using MediatR;
using TalentTrack.Application.Features.JobTitles.DTOS;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.JobTitles.Queries;

public record GetAllJobTitlesQuery() : IRequest<Result<List<JobTitleDto>>>;

