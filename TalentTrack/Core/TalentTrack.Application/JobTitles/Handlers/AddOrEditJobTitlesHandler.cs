using AutoMapper;
using MediatR;
using TalentTrack.Application.Features.JobTitles.Commands;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Features.JobTitles.Handlers;

public class AddOrEditJobTitlesHandler : SharedHandler, IRequestHandler<AddOrEditJobTitlesCommand, Result<bool>>
{

    public AddOrEditJobTitlesHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<Result<bool>> Handle(AddOrEditJobTitlesCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.BeginTransaction();
        try
        {

            var JobTitles = await GetJobTitlesAsync(request);
            if (JobTitles == null && (request.JobTitlesDto.Id != null && request.JobTitlesDto.Id != 0))
            {
                return Result<bool>.OnFail("No Data Found");
            }

            _mapper.Map(request.JobTitlesDto, JobTitles);
            await _unitOfWork.BaseRepository<JobTitle>().UpdateAsync(JobTitles!);
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

    private async Task<JobTitle?> GetJobTitlesAsync(AddOrEditJobTitlesCommand request)
    {
        if (request.JobTitlesDto.Id == null || request.JobTitlesDto.Id == 0)
        {
            return new JobTitle();
        }

        return await _unitOfWork.BaseRepository<JobTitle>().GetByIdAsync(request.JobTitlesDto.Id);
    }
}
