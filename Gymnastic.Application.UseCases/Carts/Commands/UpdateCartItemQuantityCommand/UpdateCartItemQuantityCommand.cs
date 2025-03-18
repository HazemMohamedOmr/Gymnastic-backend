using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;
using System.Text.Json.Serialization;

namespace Gymnastic.Application.UseCases.Carts.Commands.UpdateCartItemQuantityCommand
{
    public class UpdateCartItemQuantityCommand : IRequest<BaseResponse<bool>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Delta { get; set; }
    }
}
