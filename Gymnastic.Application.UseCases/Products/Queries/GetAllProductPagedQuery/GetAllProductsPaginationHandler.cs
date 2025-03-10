using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.ProductSpecs;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductPagedQuery
{
    public class GetAllProductsPaginationHandler : IRequestHandler<GetAllProductsPaginationQuery, BaseResponse<BasePagination<IEnumerable<ProductDTO>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsPaginationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<BasePagination<IEnumerable<ProductDTO>>>> Handle(GetAllProductsPaginationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var count = await _unitOfWork.Product.CountAsync();
                var skip = (request.PageNumber - 1) * request.PageSize;
                var spec = new AllProductsPaginationSpecification(skip, request.PageSize);
                var products = await _unitOfWork.Product.ListAsync(spec, cancellationToken);
                var dto = _mapper.Map<IEnumerable<ProductDTO>>(products);
                var data = new BasePagination<IEnumerable<ProductDTO>>
                {
                    Data = dto,
                    PageNumber = request.PageNumber,
                    TotalCount = count,
                    TotalPages = (count / request.PageSize) + 1,
                };
                return BaseResponse<BasePagination<IEnumerable<ProductDTO>>>.Success(data);
            }
            catch (Exception ex)
            {
                return BaseResponse<BasePagination<IEnumerable<ProductDTO>>>.Fail(ex.Message);
            }
        }
    }
}
