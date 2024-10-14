using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class TokenRepository : BaseRepository<Token>, ITokenRepository
{
    public TokenRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Token> GetTokenByValueAsync(string tokenValue)
    {
        return await _context.Tokens
            .FirstOrDefaultAsync(t => t.TokenValue == tokenValue);
    }
}
