using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Carts.Commands.AddToCartCommand
{
    public class AddToCartCommand : IRequest<BaseResponse<CartItemsDTO>>
    {
        public required int CartId { get; set; }
        public required int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
