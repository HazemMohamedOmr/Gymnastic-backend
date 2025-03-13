using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Gymnastic.Infrastructure.ImageService
{
    public class CloudinaryService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;

        public CloudinaryService(IOptions<CloudinarySettings> cloudinary)
        {
            _cloudinarySettings = cloudinary.Value;

            var account = new Account(
                    _cloudinarySettings.CloudName,
                    _cloudinarySettings.ApiKey,
                    _cloudinarySettings.ApiSecret
                );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<(string Url, string PublicId)> UploadImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return (uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }

        public async Task DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deletionParams);
        }

    }
}
