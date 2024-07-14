using Microsoft.EntityFrameworkCore.Storage;
using TalentTrack.Core.IRepository;

namespace TalentTrack.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> BaseRepository<T>() where T : class;

    Task<bool> Complete();
    Task<IDbContextTransaction> BeginTransaction();
}