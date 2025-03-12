using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Infrastructure;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.Interface.Services;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Gymnastic.Application.UseCases.Auth.Commands.RegisterCommand
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, BaseResponse<AuthDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBackgroundJobService _backgroundJobService;

        public RegisterHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTTokenService jwtTokenService,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ISendEmailService sendEmailService,
            IBackgroundJobService backgroundJobService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _backgroundJobService = backgroundJobService ?? throw new ArgumentNullException(nameof(backgroundJobService));
        }

        public async Task<BaseResponse<AuthDTO>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                if (await _userManager.FindByEmailAsync(command.Email) is not null)
                    return BaseResponse<AuthDTO>.Fail("Email is already registered!", StatusCodes.Status409Conflict);

                var user = new ApplicationUser
                {
                    Email = command.Email,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    UserName = command.FirstName + command.LastName
                };
                var result = await _userManager.CreateAsync(user, command.Password);

                if (!result.Succeeded)
                    return BaseResponse<AuthDTO>.Fail("Failed to register!", StatusCodes.Status400BadRequest, result.Errors);

                await _userManager.AddToRoleAsync(user, command.Role);
                await _unitOfWork.CommitTransactionAsync();

                _backgroundJobService.Enqueue<ISendEmailService>(service => service.EmailVericiation(user.Id));

                // TODO: Should ignore what comes next but i will do it later

                var jwtSecurityToken = await _jwtTokenService.CreateJwtToken(user);

                var authDto = new AuthDTO
                {
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { command.Role },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                return BaseResponse<AuthDTO>.Success(authDto);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BaseResponse<AuthDTO>.Fail(ex.Message);
            }
        }
    }
}
