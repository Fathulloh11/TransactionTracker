using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        => Ok(_service.CreateAsync(trxDto, GetUserId()));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TransactionCreateDto trxDto)
    {
        var updated = await _service.UpdateAsync(id, trxDto);
        return Update == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? Ok("Deleted") : NotFound();
    }
}
