using MyExpenseTracker.Models;

namespace MyExpenseTracker.DTOs;

public class TransactionReadDto
{

    // Test comment
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public Category Category { get; set; }
    public string Note { get; set; }
}