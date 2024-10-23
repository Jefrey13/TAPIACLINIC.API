using Application.Commands.Auth;
using Application.Models.ReponseDtos;
using MediatR;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;
using Application.Services;

namespace Application.Services.Impl
{
    /// <summary>
    /// Service implementation for handling authentication and password management.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling commands and queries.</param>
        /// <param name="emailSender">The service for sending emails.</param>
        /// <param name="jwtTokenService">The service for handling JWT tokens.</param>
        /// <param name="userRepository">The repository for user-related data.</param>
        public AuthService(IMediator mediator, IEmailSender emailSender, IJwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _mediator = mediator;
            _emailSender = emailSender;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<LoginResponseDto> LoginAsync(LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <inheritdoc />
        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <inheritdoc />
        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword)));
                return hashedPassword == storedHash;
            }
        }

        /// <inheritdoc />
        public async Task<bool> ChangePasswordAsync(ChangePasswordCommand command, string jwtToken)
        {
            // Extract the username from the JWT token
            var username = _jwtTokenService.GetUsernameFromToken(jwtToken);
            var user = await _userRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Change the password
            var result = await _mediator.Send(command);
            if (result)
            {
                // Send notification email
                string emailBody = "<h1>Password Changed</h1><p>Your password has been successfully updated.</p>";
                await _emailSender.SendEmailAsync(user.Email.ToString(), "Password Changed", emailBody);
            }

            return result;
        }
    }
}