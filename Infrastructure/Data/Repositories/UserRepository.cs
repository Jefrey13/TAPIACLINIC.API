
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.Value == email);  // Accedemos a la propiedad 'Value' del objeto 'Email'
    }

    public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Phone.Value == phoneNumber);  // Accedemos a la propiedad 'Value' del objeto 'PhoneNumber'
    }
}