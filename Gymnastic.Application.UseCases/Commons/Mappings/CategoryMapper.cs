using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Domain.Models;

namespace Gymnastic.Application.UseCases.Commons.Mappings
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
