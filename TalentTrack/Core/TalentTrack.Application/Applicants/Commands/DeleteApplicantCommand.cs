using MediatR;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.Applicants.Commands;

public record DeleteApplicantCommand(int Id) : IRequest<Result<bool>>;

