using Gymnastic.Application.Interface.Infrastructure;

namespace Gymnastic.Infrastructure.Helpers
{
    public class CloudinarySettings : IImageSettings
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public DefaultImageSettings DefaultProductImage { get; set; } = new DefaultImageSettings
        {
            Url = $"https://res.cloudinary.com/dqdkx3w3a/image/upload/v1742148896/default_product_1.png",
            PublicId = "default_product_1"
        };
    }
}
