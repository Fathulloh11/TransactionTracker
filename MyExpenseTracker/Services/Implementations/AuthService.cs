using Microsoft.EntityFrameworkCore;
using MyExpenseTracker.Data;
using MyExpenseTracker.Helpers;
using MyExpenseTracker.Models;
using MyExpenseTracker.Services.Interfaces;

namespace MyExpenseTracker.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return JwtAuthHelper.GenerateToken(user.Id.ToString());
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var exists = await _context.Users.AnyAsync(u => u.Username == username);
            if (exists) return false;

            var user = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
