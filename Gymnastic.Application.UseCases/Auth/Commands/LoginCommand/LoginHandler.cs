using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.Interface.Services;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using Gymnastic.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Gymnastic.Application.UseCases.Auth.Commands.LoginCommand
{
    public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse<AuthDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTTokenService jwtTokenService,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    Roles = [.. roles]
                };

                return BaseResponse<AuthDTO>.Success(authDto);
            }
            catch (Exception ex)
            {
                return BaseResponse<AuthDTO>.Fail(ex.Message);
            }
        }
    }
}
