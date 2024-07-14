using MediatR;
using TalentTrack.Application.Features.Applicants.DTOS;
using TMG.SharedKernel.Common;
using TMG.SharedKernel.Utilities;

namespace TalentTrack.Application.Features.Applicants.Queries;

public class GetApplicantByNamePaginationQuery : IRequest<Result<BaseSearchResult<List<ApplicantDto>>>>
{
    public string Name { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}