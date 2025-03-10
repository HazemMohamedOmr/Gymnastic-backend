using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Domain.Models;

namespace Gymnastic.Application.UseCases.Commons.Mappings
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
