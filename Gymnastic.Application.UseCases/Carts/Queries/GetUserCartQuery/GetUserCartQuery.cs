using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Carts.Queries.GetUserCartQuery
{
    public class GetUserCartQuery : IRequest<BaseResponse<CartDTO>>
    {
        //public string UserId { get; set; }
    }
}
