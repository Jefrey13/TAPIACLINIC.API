using Application.Commands.Auth;
using Application.Models.ReponseDtos;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using BCrypt.Net;

/// <summary>
/// Handler for processing user login requests.
/// Validates user credentials, generates JWT access and refresh tokens, and stores the refresh token.
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
    /// </summary>
    /// <param name="jwtTokenService">Service for generating JWT tokens.</param>
    /// <param name="userRepository">Repository for accessing user data.</param>
    /// <param name="tokenRepository">Repository for handling refresh tokens.</param>
    /// <param name="authService">Service for handling authentication-related operations.</param>
    public LoginCommandHandler(
        IJwtTokenService jwtTokenService,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        IAuthService authService)
    {
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _authService = authService;  // Injecting AuthService
    }

    /// <summary>
    /// Handles the login process by verifying the user's credentials and generating tokens.
    /// </summary>
    /// <param name="request">The login request containing the user's username and password.</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
    /// <returns>Returns a <see cref="LoginResponseDto"/> containing the JWT access and refresh tokens.</returns>
    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Fetch the user based on the provided username
        var user = await _userRepository.GetUserByUserNameAsync(request.Request.UserName);

        // If the user does not exist or the password is incorrect, throw an UnauthorizedAccessException
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Request.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // Generate JWT access token for the authenticated user
        var accessToken = _jwtTokenService.GenerateAccessToken(user);

        // Generate a new refresh token for the session
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        // Create a new token entity to store the refresh token
        var tokenEntity = new Token
        {
            UserId = user.Id,
            TokenValue = refreshToken,
            TokenType = "Session",
            CreatedAt = DateTime.Now,
            ExpirationDate = DateTime.Now.AddDays(7)  // Set the refresh token to expire in 7 days
        };

        // Save the refresh token to the repository
        await _tokenRepository.AddAsync(tokenEntity);

        // Return the generated tokens (access and refresh) in the response
        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}