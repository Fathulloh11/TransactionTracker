using Microsoft.EntityFrameworkCore;
using MyExpenseTracker.Data;
using MyExpenseTracker.Helpers;
using MyExpenseTracker.Models;
using MyExpenseTracker.Services.Interfaces;
using MyExpenseTracker.DTOs;

namespace MyExpenseTracker.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
            => _context = context;

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            request.Username = request.Username.ToLower();
            request.Password = request.Password.Trim();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            return JwtAuthHelper.GenerateToken(user.Id.ToString());
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            request.Username = request.Username.ToLower();
            request.Password = request.Password.Trim();

            var exists = await _context.Users.AnyAsync(u => u.Username == request.Username);
            if (exists) return false;

            _context.Users.Add(new User(request.Username, BCrypt.Net.BCrypt.HashPassword(request.Password)));
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
