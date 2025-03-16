using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.ProductSpecs;
using Microsoft.AspNetCore.Http;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Products.Queries.GetByIdProductQuery
{
    public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery, BaseResponse<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public GetByIdProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<ProductDTO>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new ProductByIdSpecification(request.Id);
                var product = await _unitOfWork.Product.GetEntityWithSpec(spec, cancellationToken);
                if (product is null)
                    return BaseResponse<ProductDTO>.Fail("Product Not Found", StatusCodes.Status404NotFound);
                var productMapped = _mapper.Map<ProductDTO>(product);
                return BaseResponse<ProductDTO>.Success(productMapped);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<ProductDTO>.Fail(ex.Message);

                return BaseResponse<ProductDTO>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
