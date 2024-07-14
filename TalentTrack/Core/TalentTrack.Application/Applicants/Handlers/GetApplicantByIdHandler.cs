using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.Applicants.DTOS;
using TalentTrack.Application.Features.Applicants.Queries;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.Applicants.Handlers;

public class GetApplicantByIdHandler : SharedHandler, IRequestHandler<GetApplicantByIdQuery, Result<ApplicantDto>>
{

    public GetApplicantByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<ApplicantDto>> Handle(GetApplicantByIdQuery request, CancellationToken cancellationToken)
    {
        var JobTitlessDto = await _unitOfWork.BaseRepository<Applicant>().GetByFilterAsync(x => x.Id == request.Id, "JobTitle");

        return JobTitlessDto != null
               ? Result<ApplicantDto>.OnSuccess(_mapper.Map<ApplicantDto>(JobTitlessDto))
               : Result<ApplicantDto>.OnFail("No Data Found");
    }

}
