using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.CategorySpecs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Categories.Queries.GetAllCategoriesQuery
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, BaseResponse<BasePagination<IEnumerable<CategoryDTO>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<BasePagination<IEnumerable<CategoryDTO>>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new AllCategoriesSpecification(
                    request.SearchTerm,
                    request.PageNumber,
                    request.PageSize,
                    request.OrderBy,
                    request.IsDecending);

                var categories = await _unitOfWork.Category.ListAsync(spec, cancellationToken);
                var count = await _unitOfWork.Category.CountAsync();

                var dto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                var response = new BasePagination<IEnumerable<CategoryDTO>>
                {
                    Data = dto,
                    PageNumber = request.PageNumber,
                    TotalPages = request.PageSize is not null ? (count / request.PageSize) + 1 : null,
                    TotalCount = count,
                };
                return BaseResponse<BasePagination<IEnumerable<CategoryDTO>>>.Success(response);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<BasePagination<IEnumerable<CategoryDTO>>>.Fail(ex.Message);

                return BaseResponse<BasePagination<IEnumerable<CategoryDTO>>>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
