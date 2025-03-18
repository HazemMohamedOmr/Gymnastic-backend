using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.CartSpecs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;


namespace Gymnastic.Application.UseCases.Carts.Commands.UpdateCartItemQuantityCommand
{
    public class UpdateCartItemQuantityHandler : IRequestHandler<UpdateCartItemQuantityCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCartItemQuantityHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<BaseResponse<bool>> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.UserId;
                if (userId is null)
                    return BaseResponse<bool>.Fail("Not Authenticated", StatusCodes.Status403Forbidden);

                var spec = new CartByUserIdSpecification(userId);
                var userCart = await _unitOfWork.Cart.GetEntityWithSpec(spec, cancellationToken);
                if (userCart is null)
                    return BaseResponse<bool>.Fail("Invalid User", StatusCodes.Status404NotFound);

                var cartItem = await _unitOfWork.CartItem.GetByIdAsync(request.Id);
                if (cartItem is null)
                    return BaseResponse<bool>.Fail("Cart item is not found", StatusCodes.Status404NotFound);

                if (cartItem.CartId != userCart.Id)
                    return BaseResponse<bool>.Fail("Invalid Operation", StatusCodes.Status409Conflict);

                var newQuantity = cartItem.Quantity + request.Delta;
                if (newQuantity < 1 && newQuantity > 5)
                    return BaseResponse<bool>.Fail("Quantity cannot be Less than 1 or Greater than 5", StatusCodes.Status400BadRequest);

                var product = await _unitOfWork.Product.GetByIdAsync(cartItem.ProductId);
                if (product!.Stock < newQuantity)
                    return BaseResponse<bool>.Fail("Insufficent Stock", StatusCodes.Status400BadRequest);

                cartItem.Quantity = newQuantity;
                await _unitOfWork.SaveAsync(cancellationToken);
                return BaseResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<bool>.Fail(ex.ToString());

                return BaseResponse<bool>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
