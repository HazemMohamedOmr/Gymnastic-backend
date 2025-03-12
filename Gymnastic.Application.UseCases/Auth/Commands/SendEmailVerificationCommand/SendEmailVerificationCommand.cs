using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;

namespace Gymnastic.Application.UseCases.Auth.Commands.SendEmailVerificationCommand
{
    public class SendEmailVerificationCommand : IRequest<BaseResponse<bool>>
    {
        public string Email { get; set; }
    }
}
