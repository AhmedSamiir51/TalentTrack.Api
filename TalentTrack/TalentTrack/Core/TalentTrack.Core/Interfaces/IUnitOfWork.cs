using Microsoft.EntityFrameworkCore.Storage;
using TalentTrack.Core.Entities;
using TalentTrack.Core.IRepository;

namespace TalentTrack.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Todo> Todos { get; }

        Task<bool> Complete();
        Task<IDbContextTransaction> BeginTransaction();
    }
}