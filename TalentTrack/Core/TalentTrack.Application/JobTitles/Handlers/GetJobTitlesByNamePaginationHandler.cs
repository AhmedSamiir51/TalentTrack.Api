using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.JobTitles.DTOS;
using TalentTrack.Application.Features.JobTitles.Queries;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;
using TMG.SharedKernel.Utilities;

namespace TalentTrack.Application.Features.JobTitles.Handlers;

public class GetJobTitlesByNamePaginationHandler : SharedHandler, IRequestHandler<GetJobTitlesByNamePaginationQuery, Result<BaseSearchResult<List<JobTitleDto>>>>
{
    public GetJobTitlesByNamePaginationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<BaseSearchResult<List<JobTitleDto>>>> Handle(GetJobTitlesByNamePaginationQuery request, CancellationToken cancellationToken)
    {

        var searchResults = await _unitOfWork.BaseRepository<JobTitle>()
                   .Search(new SearchCriteria<JobTitle>
                   {
                       Filter = p => p.Name.Contains(request.Name),
                       OrderBy = products => products.OrderBy(p => p.Id),
                       PageNumber = request.CurrentPage,
                       PageSize = request.PageSize
                   });

        return searchResults?.TotalCount > 0
         ? Result<BaseSearchResult<List<JobTitleDto>>>.OnSuccess(
             new BaseSearchResult<List<JobTitleDto>>
             {
                 Results = _mapper.Map<List<JobTitleDto>>(searchResults.Results),
                 TotalCount = searchResults.TotalCount
             })
         : Result<BaseSearchResult<List<JobTitleDto>>>.OnFail("No Data Found");
    }
}
