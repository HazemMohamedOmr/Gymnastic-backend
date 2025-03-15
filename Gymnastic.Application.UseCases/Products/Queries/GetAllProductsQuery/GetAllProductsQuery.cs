using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsQuery : IRequest<BaseResponse<BasePagination<IEnumerable<ProductDTO>>>>
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public string? OrderBy { get; set; }
        public bool IsDecending { get; set; }
    }
}
