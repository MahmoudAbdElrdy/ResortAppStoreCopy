using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity
{
    public class BaseEntity<TKey> : IEntity<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<TKey>.Default.Equals(Id, default(TKey)))
            {
                return true;
            }

            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseEntity<TKey>))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //Transient objects are not considered as equal
            var other = (BaseEntity<TKey>)obj;
            if (IsTransient() && other.IsTransient())
            {
                return false;
            }

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id == null ? 0 : Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity<TKey> left, BaseEntity<TKey> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(BaseEntity<TKey> left, BaseEntity<TKey> right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }

    public interface IFullTrackingEntity : ICreationTrackingEntity, IUpdateTrackingEntity, IDeleteTrackingEntity
    {

    }
    public interface ICreationTrackingEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
    public interface IUpdateTrackingEntity
    {
        DateTime? UpdatedAt { get; set; }
        string UpdateBy { get; set; }
    }
    public interface IDeleteTrackingEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        string DeletedBy { get; set; }
    }
    public class BaseTrackingEntity<TKey> : BaseEntity<TKey>, IFullTrackingEntity
    {
        public BaseTrackingEntity()
        {
            CreatedAt = DateTime.UtcNow;
            
        }
        public DateTime CreatedAt { get; set; }
        [MaxLength(36)]
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        [MaxLength(36)]
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        [MaxLength(300)]
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
