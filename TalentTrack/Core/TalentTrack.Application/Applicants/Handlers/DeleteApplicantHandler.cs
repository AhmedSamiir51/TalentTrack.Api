using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.Applicants.Commands;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.Applicants.Handlers;

public class DeleteApplicantHandler : SharedHandler, IRequestHandler<DeleteApplicantCommand, Result<bool>>
{

    public DeleteApplicantHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<bool>> Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
    {

        var applicant = await _unitOfWork.BaseRepository<Applicant>().GetByIdAsync(request.Id);
        if (applicant == null)
        {
            return Result<bool>.OnFail("No Data Found");
        }

        applicant.IsDeleted = true;
        await _unitOfWork.BaseRepository<Applicant>().UpdateAsync(applicant);
        await _unitOfWork.Complete();

        return Result<bool>.OnSuccess(true);
    }

}
