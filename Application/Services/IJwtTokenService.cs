namespace Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string userEmail, IEnumerable<string> roles);
        string GenerateRefreshToken();
        bool ValidateToken(string token);
        string GetPrincipalFromExpiredToken(string token);
    }
}