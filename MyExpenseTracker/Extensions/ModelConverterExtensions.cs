using MyExpenseTracker.DTOs;
using MyExpenseTracker.Models;

namespace MyExpenseTracker.Extensions
{
    public static class ModelConverterExtensions
    {
        public static Transaction ToTransaction(this TransactionCreateDto model, int userId, int id = 0)
            => id == 0 ? new()
            {
                Date = model.Date,
                Amount = model.Amount,
                Type = model.Type,
                Category = model.Category,
                Note = model.Note,
                UserId = userId
            }
            :
            new()
            {
                Id = id,
                Date = model.Date,
                Amount = model.Amount,
                Type = model.Type,
                Category = model.Category,
                Note = model.Note,
                UserId = userId
            };

        public static TransactionReadDto ToTransactionResponse(this Transaction item)
            => new()
            {
                Id = item.Id,
                Date = item.Date,
                Amount = item.Amount,
                Type = item.Type,
                Category = item.Category,
                Note = item.Note
            };
    }
}
