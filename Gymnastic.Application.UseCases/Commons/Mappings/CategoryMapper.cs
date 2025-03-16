using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Categories.Commands.CreateCategoryCommand;
using Gymnastic.Domain.Models;

namespace Gymnastic.Application.UseCases.Commons.Mappings
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
