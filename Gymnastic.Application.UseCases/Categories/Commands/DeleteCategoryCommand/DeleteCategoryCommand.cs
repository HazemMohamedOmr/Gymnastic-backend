using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
