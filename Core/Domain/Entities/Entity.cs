using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public abstract class EntityBase : EntityBase<int>, IEntity<int>
    {

    }

    /// <summary>
    /// Basic implementation of IEntity interface.
    /// An entity can inherit this class of directly implement to IEntity interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public abstract class EntityBase<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        [Key]
        public virtual TPrimaryKey Id { get; set; }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
