using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.ProductSpecs;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Queries.GetByIdProductQuery
{
    public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery, BaseResponse<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                return BaseResponse<ProductDTO>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
