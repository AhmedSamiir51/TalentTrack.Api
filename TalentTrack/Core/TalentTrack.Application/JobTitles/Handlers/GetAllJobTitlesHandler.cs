using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.JobTitles.DTOS;
using TalentTrack.Application.Features.JobTitles.Queries;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.JobTitles.Handlers;

public class GetAllJobTitlesHandler : SharedHandler, IRequestHandler<GetAllJobTitlesQuery, Result<List<JobTitleDto>>>
{

    public GetAllJobTitlesHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<List<JobTitleDto>>> Handle(GetAllJobTitlesQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.BaseRepository<JobTitle>().GetAllAsync();

        return result != null
        ? Result<List<JobTitleDto>>.OnSuccess(_mapper.Map<List<JobTitleDto>>(result))
        : Result<List<JobTitleDto>>.OnFail("No Data Found");
    }

}
