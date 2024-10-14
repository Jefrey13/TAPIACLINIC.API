using Domain.Entities;

namespace Domain.Repositories; 
public interface ITokenRepository : IRepository<Token>
{
    Task<Token> GetTokenByValueAsync(string tokenValue);
}