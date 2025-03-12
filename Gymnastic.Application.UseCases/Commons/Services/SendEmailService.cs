using Gymnastic.Application.Interface.Services;
using Gymnastic.Domain.Models;
using Gymnastic.Infrastructure.Mail;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace Gymnastic.Application.UseCases.Commons.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public SendEmailService(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService)); ;
        }

        public async Task EmailVericiation(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new InvalidOperationException($"User not found.");
            var verificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var baseUrl = "https://localhost:7130/api/auth/verify-email";
            var verificationUrl = $"{baseUrl}?email={user.Email}&token={HttpUtility.UrlEncode(verificationToken)}";
            // TODO: Remove Magic Strings

            var emailBody = $@"
                <h1>Verify Your Email</h1>
                <p>Please click the link below to verify your email address:</p>
                <a href='{verificationUrl}'>Verify Email</a>
                <p>This link will expire in 24 hours.</p>";

            await _emailService.SendEmailAsync(
                user.Email,
                "Verify Your Email Address",
                emailBody,
                null
            );
        }
    }
}
