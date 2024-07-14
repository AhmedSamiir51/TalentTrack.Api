using MediatR;
using TalentTrack.Application.SharedHandlers;
using TalentTrack.Application.Todos.Queries;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Todos.Handlers
{
    public class GetTodoHandler : SharedHandler, IRequestHandler<GetTodoQuery, Result<List<Todo>>>
    {

        public GetTodoHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Result<List<Todo>>> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Todos.GetAllAsync();
            if (result != null)
            {
                return Result<List<Todo>>.OnSuccess((List<Todo>)result);
            }

            return Result<List<Todo>>.OnFail("data");
        }

    }
}