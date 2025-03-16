using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Categories.Queries.GetByIdCategoryQuery
{
    public class GetByIdCategoryQuery : IRequest<BaseResponse<CategoryDTO>>
    {
        public required int Id { get; set; }
    }
}
