using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.JobTitles.Commands;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.JobTitles.Handlers;

public class DeleteJobTitlesHandler : SharedHandler, IRequestHandler<DeleteJobTitlesCommand, Result<bool>>
{

    public DeleteJobTitlesHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<bool>> Handle(DeleteJobTitlesCommand request, CancellationToken cancellationToken)
    {

        var JobTitles = await _unitOfWork.BaseRepository<JobTitle>().GetByIdAsync(request.Id);
        if (JobTitles == null)
        {
            return Result<bool>.OnFail("No Data Found");
        }

        var associatedApplicationsCount = await _unitOfWork.BaseRepository<Applicant>()
        .GetAllByFilterAsync(a => a.JobTitleId == request.Id);

        if (associatedApplicationsCount?.Count() > 0)
        {
            return Result<bool>.OnFail("There are applications associated with this job title.");
        }

        JobTitles.IsDeleted = true;
        await _unitOfWork.BaseRepository<JobTitle>().UpdateAsync(JobTitles);
        await _unitOfWork.Complete();
        return Result<bool>.OnSuccess(true);

    }

}
