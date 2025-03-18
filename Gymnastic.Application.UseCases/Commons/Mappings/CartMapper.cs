using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Carts.Commands.AddToCartCommand;
using Gymnastic.Domain.Models;

namespace Gymnastic.Application.UseCases.Commons.Mappings
{
    public class CartMapper : Profile
    {
        public CartMapper()
        {
            CreateMap<Cart, CartDTO>();
            CreateMap<CartItem, CartItemsDTO>().ReverseMap();
            CreateMap<AddToCartCommand, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
