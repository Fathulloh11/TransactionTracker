using MyExpenseTracker.DTOs;
using MyExpenseTracker.Models;

namespace MyExpenseTracker.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId);
        Task<Transaction?> GetByIdAsync(int id);
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<TransactionReadDto> CreateAsync(TransactionCreateDto transaction, int userId);
        Task<Transaction?> UpdateAsync(int id, Transaction transaction);
        Task<TransactionReadDto?> UpdateAsync(int id, TransactionCreateDto transaction);
        Task<bool> DeleteAsync(int id);
    }
}