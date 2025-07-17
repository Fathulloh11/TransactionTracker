using Microsoft.AspNetCore.Mvc;
using MyExpenseTracker.Services.Interfaces;

namespace MyExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("monthly")] 
        public async Task<IActionResult> GetMonthlyReport()
        {
            var report = await _reportService.GetMonthlyReportAsync();
            if (report == null)
            {
                return NotFound("Monthly report not found.");
            }
            return Ok(report);
        }
    }
}
