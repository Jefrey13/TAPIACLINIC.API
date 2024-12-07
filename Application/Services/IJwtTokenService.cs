using Application.Models.RequestDtos;
using Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Services
{
    /// <summary>
    /// Interface for managing JWT and Refresh tokens.
    /// Provides methods to generate, validate, and extract information from tokens.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT access token for a given user.
        /// The token contains user information and their assigned role as claims.
        /// </summary>
        /// <param name="user">The user entity for which the token is generated.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        string GenerateAccessToken(User user);

        /// <summary>
        /// Generates an activation token using user details.
        /// </summary>
        /// <param name="contactRequestDto">The request object containing contact details.</param>
        /// <returns>A JWT string for activation purposes.</returns>
        string GenerateActivationToken(string email);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        ClaimsPrincipal ValidateActivationToken(string token);

        /// <summary>
        /// Generates a secure refresh token, which is a random value used to refresh an expired access token.
        /// </summary>
        /// <returns>A string representing the generated refresh token.</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Extracts the ClaimsPrincipal (user identity) from an expired JWT token.
        /// This is useful for refreshing tokens, where the original token is expired but still needs to be used.
        /// </summary>
        /// <param name="token">The expired JWT token.</param>
        /// <returns>A ClaimsPrincipal containing the user identity (claims).</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);


        string GetEmailFromToken(string token);

        /// <summary>
        /// Extracts the username from a valid JWT token.
        /// </summary>
        /// <param name="token">The JWT token from which the username will be extracted.</param>
        /// <returns>A string representing the username found in the token.</returns>
        string GetUsernameFromToken(string token);

        /// <summary>
        /// Extracts the roles assigned to a user from a valid JWT token.
        /// </summary>
        /// <param name="token">The JWT token from which roles will be extracted.</param>
        /// <returns>A list of roles (strings) assigned to the user.</returns>
        IEnumerable<string> GetRolesFromToken(string token);
    }
}