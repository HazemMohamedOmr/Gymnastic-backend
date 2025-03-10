using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.ProductSpecs;
using Microsoft.AspNetCore.Http;
using MediatR;
using System.Diagnostics;

namespace Gymnastic.Application.UseCases.Products.Queries.GetByIdProductWithCategoryQuery
{
    public class GetByIdProductWithCategoryHandler : IRequestHandler<GetByIdProductWithCategoryQuery, BaseResponse<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdProductWithCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<ProductDTO>> Handle(GetByIdProductWithCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new ProductByIdWithCategorySpecification(request.Id);
                var product = await _unitOfWork.Product.GetEntityWithSpec(spec, cancellationToken);
                if (product is null)
                    return BaseResponse<ProductDTO>.Fail("Product Not Found", StatusCodes.Status404NotFound);
                var productMapped = _mapper.Map<ProductDTO>(product);
                return BaseResponse<ProductDTO>.Success(productMapped);
            }
            catch (Exception ex)
            {
                return BaseResponse<ProductDTO>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
