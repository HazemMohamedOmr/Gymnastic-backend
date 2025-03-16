using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Categories.Queries.GetAllCategoriesQuery
{
    public class GetAllCategoriesQuery : IRequest<BaseResponse<BasePagination<IEnumerable<CategoryDTO>>>>
    {
        public string? SearchTerm { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public bool IsDecending { get; set; }
    }
}
