using MediatR;
using TMG.SharedKernel.Common;
using TMG.SharedKernel.Utilities;
using TalentTrack.Application.Features.JobTitles.DTOS;

namespace TalentTrack.Application.Features.JobTitles.Queries;

public class GetJobTitlesByNamePaginationQuery : IRequest<Result<BaseSearchResult<List<JobTitleDto>>>>
{
    public string Name { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}