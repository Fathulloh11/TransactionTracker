using MyExpenseTracker.DTOs;

namespace MyExpenseTracker.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}
