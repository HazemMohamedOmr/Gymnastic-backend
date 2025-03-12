using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Auth.Commands.VerifyEmailCommand
{
    public class VerifyEmailCommand : IRequest<BaseResponse<bool>>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
