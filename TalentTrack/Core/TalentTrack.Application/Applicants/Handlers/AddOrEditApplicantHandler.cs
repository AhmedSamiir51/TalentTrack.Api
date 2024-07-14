using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.Applicants.Commands;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.Applicants.Handlers;

public class AddOrEditApplicantHandler : SharedHandler, IRequestHandler<AddOrEditApplicantCommand, Result<bool>>
{

    public AddOrEditApplicantHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<bool>> Handle(AddOrEditApplicantCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.BeginTransaction();
        try
        {
            var jobTitle = await _unitOfWork.BaseRepository<JobTitle>().GetByIdAsync(request.ApplicantDto.JobTitleId);

            if (jobTitle == null)
            {
                return Result<bool>.OnFail("Job title not found");
            }

            var currentApplicantCount = await _unitOfWork.BaseRepository<Applicant>()
               .GetAllByFilterAsync(a => a.JobTitleId == request.ApplicantDto.JobTitleId);

            // Validate against MaxApplications
            if (currentApplicantCount?.Count() >= jobTitle.MaxApplications)
            {
                return Result<bool>.OnFail("The maximum number of applications has been reached for this job title");
            }


            var applicant = await GetApplicantAsync(request);
            if (applicant == null && (request.ApplicantDto.Id != null && request.ApplicantDto.Id != 0))
            {
                return Result<bool>.OnFail("No Data Found");
            }

            _mapper.Map(request.ApplicantDto, applicant);
            await _unitOfWork.BaseRepository<Applicant>().UpdateAsync(applicant!);
            var result = await _unitOfWork.Complete();

            if (result)
            {
                transaction.Commit();
                return Result<bool>.OnSuccess(true);
            }
            else
            {
                transaction.Rollback();
                return Result<bool>.OnFail("Not Success");
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return Result<bool>.OnFail("Not Success" + ex.Message);
        }
    }

    private async Task<Applicant?> GetApplicantAsync(AddOrEditApplicantCommand request)
    {
        if (request.ApplicantDto.Id == null || request.ApplicantDto.Id == 0)
        {
            return new Applicant();
        }

        return await _unitOfWork.BaseRepository<Applicant>().GetByIdAsync(request.ApplicantDto.Id);
    }
}
