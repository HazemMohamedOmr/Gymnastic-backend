using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.CategorySpecs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Categories.Queries.GetByIdCategoryQuery
{
    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryQuery, BaseResponse<CategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public GetByIdCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<CategoryDTO>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new CategoryByIdSpecification(request.Id);
                var category = await _unitOfWork.Category.GetEntityWithSpec(spec, cancellationToken);
                if (category is null)
                    return BaseResponse<CategoryDTO>.Fail("Category not Found", StatusCodes.Status404NotFound);
                var dto = _mapper.Map<CategoryDTO>(category);
                return BaseResponse<CategoryDTO>.Success(dto);

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<CategoryDTO>.Fail(ex.Message);

                return BaseResponse<CategoryDTO>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
