using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using Gymnastic.Domain.Specification.CartSpecs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Carts.Commands.AddToCartCommand
{
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, BaseResponse<CartItemsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ICurrentUserService _currentUserService;

        public AddToCartHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<BaseResponse<CartItemsDTO>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.UserId;
                if (userId is null)
                    return BaseResponse<CartItemsDTO>.Fail("Not Authnticated", StatusCodes.Status403Forbidden);

                var spec = new CartByUserIdWithCartItemsSpecifications(userId);
                var cart = await _unitOfWork.Cart.GetEntityWithSpec(spec, cancellationToken);
                if (cart is null)
                    return BaseResponse<CartItemsDTO>.Fail("Invalid User", StatusCodes.Status404NotFound);

                if (cart.Id != request.CartId)
                    return BaseResponse<CartItemsDTO>.Fail("Invalid Cart", StatusCodes.Status409Conflict);

                var product = await _unitOfWork.Product.GetByIdAsync(request.ProductId);
                if (product is null)
                    return BaseResponse<CartItemsDTO>.Fail("Invalid Product", StatusCodes.Status409Conflict);

                if (product.Stock < request.Quantity)
                    return BaseResponse<CartItemsDTO>.Fail("Insufficent Stock", StatusCodes.Status400BadRequest);

                var productIsExist = cart.CartItems?.FirstOrDefault(c => c.ProductId == request.ProductId);
                if (productIsExist is not null)
                {
                    productIsExist.Quantity = request.Quantity;
                }
                else
                {
                    var cartItem = _mapper.Map<CartItem>(request);
                    cart.CartItems!.Add(cartItem);
                }

                await _unitOfWork.SaveAsync(cancellationToken);

                var dto = _mapper.Map<CartItemsDTO>(cart.CartItems!.Last());
                return BaseResponse<CartItemsDTO>.Success(dto);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<CartItemsDTO>.Fail(ex.ToString());

                return BaseResponse<CartItemsDTO>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
