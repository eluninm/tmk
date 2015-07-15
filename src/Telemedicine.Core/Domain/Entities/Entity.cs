using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Core.Domain.Entities
{
    /// <summary>
    /// Basic implementation of IEntity interface.
    /// An entity can inherit this class of directly implement to IEntity interface.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity.</typeparam>
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        public virtual TKey Id { get; set; }
    }

    /// <summary>
    /// A shortcut of <see cref="Entity{TKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class Entity : Entity<int>, IEntity
    {

    }
}
