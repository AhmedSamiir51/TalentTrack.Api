using MediatR;
using TalentTrack.Application.Features.JobTitles.DTOS;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.JobTitles.Commands;

public record AddOrEditJobTitlesCommand(AddOrEditJobTitleDto JobTitlesDto) : IRequest<Result<bool>>;
