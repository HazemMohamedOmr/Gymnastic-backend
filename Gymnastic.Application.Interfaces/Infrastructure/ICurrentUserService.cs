namespace Gymnastic.Application.Interface.Infrastructure
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }
        bool IsAuthenticated { get; }
    }
}
