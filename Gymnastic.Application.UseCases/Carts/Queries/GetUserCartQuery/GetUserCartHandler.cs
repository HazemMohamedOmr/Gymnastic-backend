using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.CartSpecs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Carts.Queries.GetUserCartQuery
{
    public class GetUserCartHandler : IRequestHandler<GetUserCartQuery, BaseResponse<CartDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ICurrentUserService _currentUserService;

        public GetUserCartHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<BaseResponse<CartDTO>> Handle(GetUserCartQuery requset, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.UserId;
                if (userId is null)
                    return BaseResponse<CartDTO>.Fail("Not Authnticated", StatusCodes.Status403Forbidden);

                var spec = new CartByUserIdWithAllSpecifications(userId);
                var cart = await _unitOfWork.Cart.GetEntityWithSpec(spec, cancellationToken);
                if (cart is null)
                    return BaseResponse<CartDTO>.Fail("Invalid User");

                var dto = _mapper.Map<CartDTO>(cart);
                return BaseResponse<CartDTO>.Success(dto);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<CartDTO>.Fail(ex.ToString());

                return BaseResponse<CartDTO>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
