using Microsoft.AspNetCore.Http;

namespace Gymnastic.Application.Interface.Infrastructure
{
    public interface IImageService
    {
        Task<(string Url, string PublicId)> UploadImageAsync(IFormFile file, string folder);
        Task DeleteImageAsync(string publicId);
    }
}
