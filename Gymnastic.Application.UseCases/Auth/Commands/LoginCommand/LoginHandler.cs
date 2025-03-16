using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Services;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;

namespace Gymnastic.Application.UseCases.Auth.Commands.LoginCommand
{
    public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse<AuthDTO>>
    {
        private readonly IJWTTokenService _jwtTokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public LoginHandler(
            IJWTTokenService jwtTokenService,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<AuthDTO>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(command.Email);

                if (user is null || !await _userManager.CheckPasswordAsync(user, command.Password))
                    return BaseResponse<AuthDTO>.Fail("Email or Password is incorrect!", StatusCodes.Status401Unauthorized);

                if (user.EmailConfirmed is false)
                    return BaseResponse<AuthDTO>.Fail(
                        "Please verify your email before logging in. Check your inbox for a verification link.",
                        StatusCodes.Status403Forbidden);

                var jwtSecurityToken = await _jwtTokenService.CreateJwtToken(user);

                var roles = await _userManager.GetRolesAsync(user);

                var authDto = new AuthDTO
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    Roles = [.. roles]
                };

                return BaseResponse<AuthDTO>.Success(authDto);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<AuthDTO>.Fail(ex.Message);

                return BaseResponse<AuthDTO>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
