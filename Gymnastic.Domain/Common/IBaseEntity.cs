namespace Gymnastic.Domain.Common
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}