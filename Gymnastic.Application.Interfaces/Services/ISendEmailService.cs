using Gymnastic.Domain.Models;

namespace Gymnastic.Application.Interface.Services
{
    public interface ISendEmailService
    {
        Task EmailVericiation(string userId);
    }
}
