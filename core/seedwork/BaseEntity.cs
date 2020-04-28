using System;
using System.ComponentModel.DataAnnotations;

namespace core.seedwork
{
    public interface IBaseEntity
    {

    }

    public interface IBaseEntity<T> : IBaseEntity
    {
        /// <summary>
        /// Identificador
        /// </summary>
        T Id { get; set; }
    }

    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        /// <summary>
        /// Identificador
        /// </summary>
        [Key]
        [Required]
        public T Id { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt{ get; set; }

        public static bool operator ==(BaseEntity<T> left, BaseEntity<T> right)
        {
            if (Equals(left, null))
                return (Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(BaseEntity<T> left, BaseEntity<T> right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseEntity<T>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = (BaseEntity<T>)obj;

            return item.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
    }
}
