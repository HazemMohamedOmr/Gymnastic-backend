using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductPagedQuery
{
    public class GetAllProductsPaginationQuery : IRequest<BaseResponse<BasePagination<IEnumerable<ProductDTO>>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
