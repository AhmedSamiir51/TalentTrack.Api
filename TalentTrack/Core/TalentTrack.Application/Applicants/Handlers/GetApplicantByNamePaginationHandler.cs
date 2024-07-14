using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.Applicants.DTOS;
using TalentTrack.Application.Features.Applicants.Queries;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;
using TMG.SharedKernel.Utilities;

namespace TalentTrack.Application.Features.Applicants.Handlers;

public class GetApplicantByNamePaginationHandler : SharedHandler, IRequestHandler<GetApplicantByNamePaginationQuery, Result<BaseSearchResult<List<ApplicantDto>>>>
{
    public GetApplicantByNamePaginationHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<BaseSearchResult<List<ApplicantDto>>>> Handle(GetApplicantByNamePaginationQuery request, CancellationToken cancellationToken)
    {

        var searchResults = await _unitOfWork.BaseRepository<Applicant>()
                   .Search(new SearchCriteria<Applicant>
                   {
                       Filter = p => p.Name.Contains(request.Name),
                       OrderBy = products => products.OrderBy(p => p.Id),
                       PageNumber = request.CurrentPage,
                       PageSize = request.PageSize
                   }, "JobTitle");

        return searchResults?.TotalCount > 0
         ? Result<BaseSearchResult<List<ApplicantDto>>>.OnSuccess(
             new BaseSearchResult<List<ApplicantDto>>
             {
                 Results = _mapper.Map<List<ApplicantDto>>(searchResults.Results),
                 TotalCount = searchResults.TotalCount
             })
         : Result<BaseSearchResult<List<ApplicantDto>>>.OnFail("No Data Found");
    }
}
