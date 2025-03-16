using Gymnastic.Application.Dto.Contracts.Requests;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<BaseResponse<ProductDTO>>
    {
        public CreateProductRequest Product { get; set; }
        public CreateProductCommand(CreateProductRequest product)
        {
            Product = product;
        }
    }
}
