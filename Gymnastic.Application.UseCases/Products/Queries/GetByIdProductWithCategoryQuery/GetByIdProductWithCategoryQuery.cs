using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Queries.GetByIdProductWithCategoryQuery
{
    public class GetByIdProductWithCategoryQuery : IRequest<BaseResponse<ProductDTO>>
    {
        public int Id { get; set; }
    }
}
