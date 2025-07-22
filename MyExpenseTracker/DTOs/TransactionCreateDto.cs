using System.ComponentModel.DataAnnotations;
using MyExpenseTracker.Models;

namespace MyExpenseTracker.DTOs;

public class TransactionCreateDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    public Category Category { get; set; }

    public string Note { get; set; }

    [Required]
    public int UserId { get; set; } 
}