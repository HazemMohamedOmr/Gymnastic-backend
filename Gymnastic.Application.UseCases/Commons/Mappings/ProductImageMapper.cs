using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Domain.Models;

namespace Gymnastic.Application.UseCases.Commons.Mappings
{
    public class ProductImageMapper : Profile
    {
        public ProductImageMapper()
        {
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
        }
    }
}
