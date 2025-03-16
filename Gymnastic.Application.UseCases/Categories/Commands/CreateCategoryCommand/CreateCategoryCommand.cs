using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryCommand : IRequest<BaseResponse<CategoryDTO>>
    {
        public string Name { get; set; }
    }
}
