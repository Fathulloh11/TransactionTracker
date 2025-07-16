using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyExpenseTracker.Models;
using MyExpenseTracker.Services.Interfaces;

namespace MyExpenseTracker.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service) => _service = service;

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<IActionResult> GetMy()
    {
        var list = await _service.GetByUserIdAsync(GetUserId());
        return Ok(list);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Transaction trx)
    {
        trx.UserId = GetUserId();
        var result = await _service.CreateAsync(trx);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? Ok("Deleted") : NotFound();
    }
}