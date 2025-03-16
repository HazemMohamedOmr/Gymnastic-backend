using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Gymnastic.Application.UseCases.Products.Commands.CreateProductCommand
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, BaseResponse<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IImageSettings _imageSettings;
        private readonly IWebHostEnvironment _environment;

        public CreateProductHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageService imageService,
            IWebHostEnvironment environment,
            IImageSettings imageSettings)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _imageSettings = imageSettings ?? throw new ArgumentNullException(nameof(imageSettings));
        }

        public async Task<BaseResponse<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _unitOfWork.Category.GetByIdAsync(request.Product.CategoryId) is null)
                    return BaseResponse<ProductDTO>.Fail("Category is invalid");

                var product = _mapper.Map<Product>(request.Product);
                product.Images = new List<ProductImage>();

                if (request.Product.Images != null && request.Product.Images.Any())
                {
                    bool isFirstImage = true;
                    foreach (var image in request.Product.Images)
                    {
                        var (url, publicId) = await _imageService.UploadImageAsync(
                            image,
                            "products"
                        );

                        product.Images.Add(new ProductImage
                        {
                            ImageUrl = url,
                            PublicId = publicId,
                            IsPrimary = isFirstImage,
                            Product = product
                        });
                        isFirstImage = false;
                    }
                }
                else
                {
                    product.Images.Add(new ProductImage
                    {
                        ImageUrl = _imageSettings.DefaultProductImage.Url,
                        PublicId = _imageSettings.DefaultProductImage.PublicId,
                        IsPrimary = true,
                        Product = product
                    });
                }

                await _unitOfWork.Product.AddAsync(product);
                await _unitOfWork.SaveAsync(cancellationToken);

                var dto = _mapper.Map<ProductDTO>(product);
                return BaseResponse<ProductDTO>.Success(dto);
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
