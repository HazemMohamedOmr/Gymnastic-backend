using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Carts.Commands.DeleteCartItemCommand
{
    public class DeleteCartItemCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}
