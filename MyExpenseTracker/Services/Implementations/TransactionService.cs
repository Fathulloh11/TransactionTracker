using Microsoft.EntityFrameworkCore;
using MyExpenseTracker.Data;
using MyExpenseTracker.Models;
using MyExpenseTracker.Services.Interfaces;

namespace MyExpenseTracker.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == transaction.UserId);
            if (!userExists)
                throw new Exception("User does not exist.");

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<Transaction?> UpdateAsync(int id, Transaction transaction)
        {
            if (id != transaction.Id) return null;

            var existing = await _context.Transactions.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(transaction);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
