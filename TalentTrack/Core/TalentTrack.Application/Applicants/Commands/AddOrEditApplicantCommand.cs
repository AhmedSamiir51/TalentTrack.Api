using MediatR;
using TalentTrack.Application.Features.Applicants.DTOS;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.Applicants.Commands;

public record AddOrEditApplicantCommand(AddOrEditApplicantDto ApplicantDto) : IRequest<Result<bool>>;
