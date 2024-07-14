
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TalentTrack.Core.IRepository;
using TalentTrack.Infrastructure.Data;
using TMG.SharedKernel.Utilities;

namespace TalentTrack.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected AppDbContext _context;
        protected DbSet<T> table { get; set; }

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<BaseSearchResult<IEnumerable<T>>> Search(SearchCriteria<T> criteria, string includeProperties = "")
        {
            var result = new BaseSearchResult<IEnumerable<T>>
            {
                TotalCount = 0,
                Results = Enumerable.Empty<T>()
            };

            IQueryable<T> query = _context.Set<T>();

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (criteria.Filter != null)
            {
                query = query.Where(criteria.Filter);
            }

            result.TotalCount = await query.CountAsync();

            if (criteria.OrderBy != null)
            {
                query = criteria.OrderBy(query);
            }

            if (criteria.PageNumber != null && criteria.PageNumber > 0)
            {
                if (criteria.PageSize == null || criteria.PageSize > 100)
                {
                    criteria.PageSize = 100;
                }

                query = query.Skip(((int)criteria.PageNumber - 1) * (int)criteria.PageSize)
                             .Take((int)criteria.PageSize);
            }

            result.Results = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string includeProperties = "")
        {
            if (includeProperties == "")
            {
                return await table.ToListAsync();
            }
            else
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var includeProperty in includeProperties.Split
                   (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                return await query.ToListAsync();
            }
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> Filter, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();
            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> Filter, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();
            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync();
        }


        public async Task<T?> GetByIdAsync(params object[] id)
        {
            return await table.FindAsync(id);
        }

        public IEnumerable<T> GetAllIncludes(params Expression<Func<T, object>>[] properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            IQueryable<T> query = _context.Set<T>();
            query = properties
                       .Aggregate(query, (current, property) => current.Include(property));

            return query.AsNoTracking().ToList(); //readonly
        }

        public async Task AddAsync(T obj)
        {
            await table.AddAsync(obj);
        }
        public async Task AddRangeAsync(IEnumerable<T> obj)
        {
            await table.AddRangeAsync(obj);
        }

        public async Task UpdateAsync(T obj)
        {
            await Task.Run(() => table.Update(obj));
        }
        public async Task UpdateRangeAsync(IEnumerable<T> obj)
        {
            await Task.Run(() => table.UpdateRange(obj));
        }
        public async Task DeleteAsync(T obj)
        {
            await Task.Run(() => table.Remove(obj));
        }
        public async Task DeleteRangeAsync(IEnumerable<T> obj)
        {
            await Task.Run(() => table.RemoveRange(obj));
        }


    }
}