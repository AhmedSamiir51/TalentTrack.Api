using MediatR;
using TalentTrack.Application.Features.Applicants.DTOS;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.Applicants.Queries;

public record GetAllApplicantQuery() : IRequest<Result<List<ApplicantDto>>>;

