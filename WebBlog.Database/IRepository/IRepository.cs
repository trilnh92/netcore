using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebBlog.Database.IRepository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Query { get; }

        /* Synchronous */
        TEntity Find(params object[] keys);
        TEntity Find(Expression<Func<TEntity, bool>> predicate = null);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");   
        void Insert(TEntity entity);       
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(TEntity entity, string[] properties);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void SaveChanges();

        /* Asynchronous */
        Task<TEntity> FindAsync(params object[] keys);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, string[] properties);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        Task SaveChangesAsync();
    }
}
