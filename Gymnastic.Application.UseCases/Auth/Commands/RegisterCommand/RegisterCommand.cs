using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Auth.Commands.RegisterCommand
{
    public class RegisterCommand : IRequest<BaseResponse<AuthDTO>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
