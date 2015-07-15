using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Core.Domain.Entities;

namespace Telemedicine.Core.Domain.Repositories
{
    /// <summary>
    /// This interface should be implemented by repositories to identify them by convention.
    /// </summary>
    public interface IRepository
    {
    }

    /// <summary>
    /// This interface is implemented by repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TKey">Primary key type of the entity</typeparam>
    public interface IRepository<TEntity, in TKey> : IRepository
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity.</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity.</param>
        /// <returns>Entity</returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Gets collection of all entities.
        /// </summary>
        /// <returns>Collection of entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Updates an existing entity. 
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        void Delete(TKey id);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(TEntity entity);
    }

    /// <summary>
    /// A shortcut of <see cref="IRepository{TEntity,TKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}
