using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.ProductSpecs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, BaseResponse<BasePagination<IEnumerable<ProductDTO>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<BasePagination<IEnumerable<ProductDTO>>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new AllProductsSpecificaitons(
                    request.SearchTerm,
                    request.CategoryId,
                    request.MinPrice,
                    request.MaxPrice,
                    request.pageNumber,
                    request.pageSize,
                    request.OrderBy,
                    request.IsDecending);

                var products = await _unitOfWork.Product.ListAsync(spec, cancellationToken);
                var count = await _unitOfWork.Product.CountAsync();
                var dto = _mapper.Map<IEnumerable<ProductDTO>>(products);
                var response = new BasePagination<IEnumerable<ProductDTO>>
                {
                    Data = dto,
                    PageNumber = request.pageNumber,
                    TotalPages = request.pageSize is not null ? (count / request.pageSize) + 1 : null, // TODO: Divide be zero in validator
                    TotalCount = count,
                };
                return BaseResponse<BasePagination<IEnumerable<ProductDTO>>>.Success(response);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<BasePagination<IEnumerable<ProductDTO>>>.Fail(ex.Message);

                return BaseResponse<BasePagination<IEnumerable<ProductDTO>>>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
