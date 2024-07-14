

using Microsoft.EntityFrameworkCore.Storage;
using TalentTrack.Core.Interfaces;
using TalentTrack.Core.IRepository;
using TalentTrack.Infrastructure.Data;
using TalentTrack.Infrastructure.Repositories;

namespace TalentTrack.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        IBaseRepository<T> IUnitOfWork.BaseRepository<T>() => new BaseRepository<T>(_context);

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Complete()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_context == null)
            {
                return;
            }

            _context.Dispose();
        }
    }
}