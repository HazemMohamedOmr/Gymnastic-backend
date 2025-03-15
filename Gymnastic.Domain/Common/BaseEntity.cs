namespace Gymnastic.Domain.Common
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}