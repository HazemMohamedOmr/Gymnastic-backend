using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.Interface.Services;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;

namespace Gymnastic.Application.UseCases.Auth.Commands.SendEmailVerificationCommand
{
    public class SendEmailVerificationHandler : IRequestHandler<SendEmailVerificationCommand, BaseResponse<bool>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly IWebHostEnvironment _environment;

        public SendEmailVerificationHandler(UserManager<ApplicationUser> userManager, IBackgroundJobService backgroundJobService, IWebHostEnvironment environment)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _backgroundJobService = backgroundJobService ?? throw new ArgumentNullException(nameof(backgroundJobService));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<bool>> Handle(SendEmailVerificationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(command.Email);
                if (user is null)
                    return BaseResponse<bool>.Fail("User not found", StatusCodes.Status404NotFound);

                if (user.EmailConfirmed)
                    return BaseResponse<bool>.Fail("Email is already verified", StatusCodes.Status400BadRequest);

                _backgroundJobService.Enqueue<ISendEmailService>(service => service.EmailVericiation(user.Id));

                return BaseResponse<bool>.Success(true, "Verification email resent successfully");
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<bool>.Fail($"Failed to resend verification email: {ex.Message}", StatusCodes.Status500InternalServerError);

                return BaseResponse<bool>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
