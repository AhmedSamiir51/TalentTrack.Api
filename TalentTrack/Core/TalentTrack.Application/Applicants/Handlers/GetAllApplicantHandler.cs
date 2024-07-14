using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.Applicants.DTOS;
using TalentTrack.Application.Features.Applicants.Queries;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.Applicants.Handlers;

public class GetAllApplicantHandler : SharedHandler, IRequestHandler<GetAllApplicantQuery, Result<List<ApplicantDto>>>
{

    public GetAllApplicantHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<List<ApplicantDto>>> Handle(GetAllApplicantQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.BaseRepository<Applicant>().GetAllAsync("JobTitle");

        return result != null
        ? Result<List<ApplicantDto>>.OnSuccess(_mapper.Map<List<ApplicantDto>>(result))
        : Result<List<ApplicantDto>>.OnFail("No Data Found");
    }

}
