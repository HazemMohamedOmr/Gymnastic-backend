using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsQuery : IRequest<BaseResponse<IEnumerable<ProductDTO>>>
    {
    }
}
