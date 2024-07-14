using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.JobTitles.DTOS;
using TalentTrack.Application.Features.JobTitles.Queries;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.JobTitles.Handlers;

public class GetJobTitlesByIdHandler : SharedHandler, IRequestHandler<GetJobTitlesByIdQuery, Result<JobTitleDto>>
{

    public GetJobTitlesByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<JobTitleDto>> Handle(GetJobTitlesByIdQuery request, CancellationToken cancellationToken)
    {
        var JobTitlessDto = await _unitOfWork.BaseRepository<JobTitle>().GetByFilterAsync(x => x.Id == request.Id);

        return JobTitlessDto != null
               ? Result<JobTitleDto>.OnSuccess(_mapper.Map<JobTitleDto>(JobTitlessDto))
               : Result<JobTitleDto>.OnFail("No Data Found");
    }

}
