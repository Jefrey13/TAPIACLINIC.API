using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.Models.RequestDtos;

namespace Application.Services.Impl
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // Agregar el único rol como claim
            if (user.Role != null)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(30),  // Expiración del token
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine("Generated Token: " + jwt);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public string GetUsernameFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        /// <summary>
        /// Extracts the email address from a JWT token.
        /// </summary>
        /// <param name="token">The JWT token containing the user's claims.</param>
        /// <returns>The email address if present, otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown if the token is invalid or cannot be read.</exception>
        public string GetEmailFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("El token no puede estar vacío o nulo.", nameof(token));
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Read the token without validating it (useful if the token is expired but still needs claims extraction)
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken == null)
                {
                    throw new ArgumentException("El token proporcionado no es válido.");
                }

                // Extract the email claim
                //var emailClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                // Extract the email claim
                var emailClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;

                if (string.IsNullOrEmpty(emailClaim))
                {
                    throw new ArgumentException("El token no contiene un correo electrónico válido.");
                }

                return emailClaim;
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, "Error al intentar obtener el correo electrónico del token.");
                throw new ArgumentException("No se pudo extraer el correo electrónico del token. Verifica el formato del token.", ex);
            }
        }


        public IEnumerable<string> GetRolesFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        }

        /// <summary>
        /// Generates a JWT activation token with user information.
        /// </summary>
        /// <param name="contactRequestDto">The request object containing contact details.</param>
        /// <returns>A JWT token string.</returns>
        public string GenerateActivationToken(string email)
        {
            // Define claims for the JWT token
            var authClaims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Email, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // Create signing key from configuration
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(3), // Token expiration time
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // Write and return the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            //_logger?.LogInformation("Generated JWT Token: {Token}", jwt);

            return jwt;
        }

        /// <summary>
        /// Retrieves the claims principal from an expired token.
        /// </summary>
        /// <param name="token">The JWT token to validate and extract claims from.</param>
        /// <returns>The claims principal extracted from the token.</returns>
        /// <exception cref="SecurityTokenException">Thrown if the token is invalid or the algorithm is incorrect.</exception>
        public ClaimsPrincipal ValidateActivationToken(string token)
        {
            // Define token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = false // Ignore token expiration for this method
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validate the token and extract the principal
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("El token no es válido o no se generó correctamente.");
                }

                return principal;
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, "Error al validar el token.");
                throw new SecurityTokenException("Hubo un problema al procesar el token. Por favor, verifica el enlace de activación.");
            }
        }


    }
}