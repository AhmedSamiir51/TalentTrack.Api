using AutoMapper;
using TalentTrack.Core.Interfaces;

namespace TalentTrack.Application.SharedHandlers;

public abstract class SharedHandler
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    protected SharedHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
}
