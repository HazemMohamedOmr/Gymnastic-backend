using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommand : IRequest<BaseResponse<CategoryDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
