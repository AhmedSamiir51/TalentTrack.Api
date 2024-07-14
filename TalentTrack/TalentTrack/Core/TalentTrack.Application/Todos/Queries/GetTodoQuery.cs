using MediatR;
using TalentTrack.Core.Entities;
using TMG.SharedKernel.Common;

namespace TalentTrack.Application.Todos.Queries
{
    public record GetTodoQuery() : IRequest<Result<List<Todo>>>;

}
