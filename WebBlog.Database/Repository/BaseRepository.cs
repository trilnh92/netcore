using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Database.IRepository;

namespace WebBlog.Database.Repository
{
    public abstract class BaseRepository<TEntity, DbContextT> : IRepository<TEntity> where TEntity : class where DbContextT : DbContext
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;
        private bool _saveChangesImmediatly;

        internal BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _saveChangesImmediatly = true;
        }

        internal BaseRepository(DbContext context, bool saveChangesImmediatly = true)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _saveChangesImmediatly = saveChangesImmediatly;
        }

        public IQueryable<TEntity> Query { get => _dbSet; }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Synchronous
        public TEntity Find(params object[] keys)
        {
            return _dbSet.Find(keys);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = Query;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);

            if (_saveChangesImmediatly)
                _context.SaveChanges();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);

            if (_saveChangesImmediatly)
                _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            if (_saveChangesImmediatly)
                _context.SaveChanges();
        }

        public void Update(TEntity entity, string[] properties)
        {
            properties.ToList().ForEach(p => _context.Entry(entity).Property(p).IsModified = true);

            if (_saveChangesImmediatly)
                _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            if (_saveChangesImmediatly)
                _context.SaveChanges();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                return;

            var detachedEntities = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Detached)
                .Select(x => x.Entity).Where(x => x.GetType() == typeof(TEntity))
                .Cast<TEntity>().ToList();

            foreach (var entity in entities)
            {
                if (detachedEntities.Contains(entity))
                {
                    _dbSet.Attach(entity);
                }
            }

            _dbSet.RemoveRange(entities);

            if (_saveChangesImmediatly)
                _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        #endregion

        #region Asynchronous
        public async Task<TEntity> FindAsync(params object[] keys)
        {
            return await _dbSet.FindAsync(keys);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = Query;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task InsertAsync(TEntity entity)
        {
            _dbSet.Add(entity);

            if (_saveChangesImmediatly)
                await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);

            if (_saveChangesImmediatly)
                await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            if (_saveChangesImmediatly)
                await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity, string[] properties)
        {
            properties.ToList().ForEach(p => _context.Entry(entity).Property(p).IsModified = true);

            if (_saveChangesImmediatly)
                await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            if (_saveChangesImmediatly)
                await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                return;

            var detachedEntities = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Detached)
                .Select(x => x.Entity).Where(x => x.GetType() == typeof(TEntity))
                .Cast<TEntity>().ToList();

            foreach (var entity in entities)
            {
                if (detachedEntities.Contains(entity))
                {
                    _dbSet.Attach(entity);
                }
            }

            _dbSet.RemoveRange(entities);

            if (_saveChangesImmediatly)
                await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
