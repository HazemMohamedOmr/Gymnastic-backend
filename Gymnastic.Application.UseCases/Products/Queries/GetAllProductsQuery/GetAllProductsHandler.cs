using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.ProductSpecs;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, BaseResponse<IEnumerable<ProductDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<IEnumerable<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new AllProductsSpecification();
                var products = await _unitOfWork.Product.ListAsync(spec, cancellationToken);
                var dto = _mapper.Map<IEnumerable<ProductDTO>>(products);
                return BaseResponse<IEnumerable<ProductDTO>>.Success(dto);
            }
            catch (Exception ex)
            {
                return BaseResponse<IEnumerable<ProductDTO>>.Fail(ex.Message);
            }
        }
    }
}
