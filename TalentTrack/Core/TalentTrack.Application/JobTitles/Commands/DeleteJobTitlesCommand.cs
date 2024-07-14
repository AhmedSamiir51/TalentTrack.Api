using MediatR;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.JobTitles.Commands;

public record DeleteJobTitlesCommand(int Id) : IRequest<Result<bool>>;

