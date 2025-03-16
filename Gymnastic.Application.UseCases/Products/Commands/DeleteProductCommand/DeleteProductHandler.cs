using AutoMapper;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Specification.ProductImagesSpecs;
using Gymnastic.Domain.Specification.ProductSpecs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Products.Commands.DeleteProductCommand
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public DeleteProductHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var spec = new GetProductByIdWithImagesSpecifications(request.Id);
                var product = await _unitOfWork.Product.GetEntityWithSpec(spec);
                if (product is null)
                    return BaseResponse<bool>.Fail("Product not found", StatusCodes.Status404NotFound);

                _unitOfWork.Product.Delete(product);

                if(product.Images is not null)
                    _unitOfWork.ProductImage.DeleteRange(product.Images!);

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
