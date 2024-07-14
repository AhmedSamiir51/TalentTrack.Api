using System.Linq.Expressions;
using TMG.SharedKernel.Utilities;

namespace TalentTrack.Core.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<BaseSearchResult<IEnumerable<T>>> Search(SearchCriteria<T> criteria, string includeProperties = "");
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = "");
        Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> Filter, string includeProperties = "");
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> Filter, string includeProperties = "");
        Task<T?> GetByIdAsync(params object[] id);
        IQueryable<T> GetTableNoTracking();
        IEnumerable<T> GetAllIncludes(params Expression<Func<T, object>>[] properties);
        Task AddAsync(T obj);
        Task AddRangeAsync(IEnumerable<T> obj);
        Task UpdateAsync(T obj);
        Task UpdateRangeAsync(IEnumerable<T> obj);

        Task DeleteAsync(T obj);
        Task DeleteRangeAsync(IEnumerable<T> obj);

    }
}