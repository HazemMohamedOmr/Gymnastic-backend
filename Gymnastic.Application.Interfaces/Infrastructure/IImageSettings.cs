
namespace Gymnastic.Application.Interface.Infrastructure
{
    public interface IImageSettings
    {
        DefaultImageSettings DefaultProductImage { get; }
    }

    public class DefaultImageSettings
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}
