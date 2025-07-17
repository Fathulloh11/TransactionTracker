using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenseTracker.Models;
using MyExpenseTracker.Services.Interfaces;
using MyExpenseTracker.DTOs;

namespace MyExpenseTracker.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<IActionResult> GetMy()
    {
        var list = await _service.GetByUserIdAsync(GetUserId());
        
        var dtoList = list.Select(t => new TransactionReadDto
        {
            Id = t.Id,
            Date = t.Date,
            Amount = t.Amount,
            Type = t.Type,
            Category = t.Category,
            Note = t.Note
        });

        return Ok(dtoList);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransactionCreateDto trxDto)
    {
        var trx = new Transaction
        {
            Date = trxDto.Date,
            Amount = trxDto.Amount,
            Type = trxDto.Type,
            Category = trxDto.Category,
            Note = trxDto.Note,
            UserId = GetUserId()
        };

        var result = await _service.CreateAsync(trx);

        var dto = new TransactionReadDto
        {
            Id = result.Id,
            Date = result.Date,
            Amount = result.Amount,
            Type = result.Type,
            Category = result.Category,
            Note = result.Note
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TransactionCreateDto trxDto)
    {
        var trx = new Transaction
        {
            Id = id,
            Date = trxDto.Date,
            Amount = trxDto.Amount,
            Type = trxDto.Type,
            Category = trxDto.Category,
            Note = trxDto.Note,
            UserId = GetUserId()
        };

        var updated = await _service.UpdateAsync(id, trx);
        if (updated == null)
            return NotFound();

        var dto = new TransactionReadDto
        {
            Id = updated.Id,
            Date = updated.Date,
            Amount = updated.Amount,
            Type = updated.Type,
            Category = updated.Category,
            Note = updated.Note
        };

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? Ok("Deleted") : NotFound();
    }
}
