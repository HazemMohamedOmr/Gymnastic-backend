using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;

namespace Gymnastic.Application.UseCases.Auth.Commands.VerifyEmailCommand
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, BaseResponse<bool>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public VerifyEmailHandler(UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<bool>> Handle(VerifyEmailCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(command.Email);
                if (user == null)
                    return BaseResponse<bool>.Fail("User not found", StatusCodes.Status404NotFound);

                if (user.EmailConfirmed)
                    return BaseResponse<bool>.Success(true, "Email already verified");

                var result = await _userManager.ConfirmEmailAsync(user, command.Token);
                if (!result.Succeeded)
                    return BaseResponse<bool>.Fail("Invalid verification token", StatusCodes.Status400BadRequest, result.Errors);

                user.EmailConfirmed = true;
                user.UpdatedAt = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                return BaseResponse<bool>.Success(true, "Email verified successfully");
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<bool>.Fail($"Verification failed: {ex.Message}");

                return BaseResponse<bool>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
