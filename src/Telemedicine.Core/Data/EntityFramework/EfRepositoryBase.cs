using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;
using Telemedicine.Core.Domain.Repositories;

namespace Telemedicine.Core.Data.EntityFramework
{
    public class EfRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IDbContextProvider _dbContextProvider;

        public EfRepositoryBase(IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        protected DataContext DbContext { get { return _dbContextProvider.Context; } }

        protected DbSet<TEntity> Set { get { return DbContext.Set<TEntity>(); } }

        public virtual TEntity GetById(int id)
        {
            return Set.Find(id);
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return Set.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return Set.Add(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = Set.Find(id);

            if (entity == null)
            {
                return;
            }

            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Set.Remove(entity);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Set.Local.Contains(entity))
            {
                Set.Attach(entity);
            }
        }
    }
}
