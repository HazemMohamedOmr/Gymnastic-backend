using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Auth.Commands.LoginCommand
{
    public class LoginCommand : IRequest<BaseResponse<AuthDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
