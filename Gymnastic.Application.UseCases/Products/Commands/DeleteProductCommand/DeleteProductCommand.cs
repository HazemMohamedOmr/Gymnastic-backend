using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
