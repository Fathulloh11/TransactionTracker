using Microsoft.EntityFrameworkCore;
using MyExpenseTracker.Data;
using MyExpenseTracker.Models;
using MyExpenseTracker.Services.Interfaces;

namespace MyExpenseTracker.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        public ReportService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<object> GetMonthlyReportAsync()
        {
            var now = DateTime.Now;
            var transactions = await _context.Transactions
                .Where(t => t.Date.Month == now.Month && t.Date.Year == now.Year)
                .ToListAsync();

            if (!transactions.Any())
                return null;

            var income = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expenses = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);    
            var balance = income - expenses;

            var categorySummary = transactions
                .GroupBy(t => t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(t => t.Amount)
                });

            var maxExpense = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .OrderByDescending(t => t.Amount)
                .FirstOrDefault();

            return new
            {
                Month = now.ToString("dd/MM/yyyy"), 
                income = income,                    
                expenses = expenses,              
                balance = balance,                   
                ByCategory = categorySummary,   
               MaxExpense = maxExpense == null ? null : new
                {
                    Amount = maxExpense.Amount,             
                    Category = maxExpense.Category.ToString(),
                    Note = maxExpense.Note,
                    Date = maxExpense.Date.ToShortDateString() 
                }
            };

        }
    }
}
