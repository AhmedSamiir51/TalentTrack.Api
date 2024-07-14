using TalentTrack.Core.Interfaces;

namespace TalentTrack.Application.SharedHandlers
{
    public abstract class SharedHandler
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected SharedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
