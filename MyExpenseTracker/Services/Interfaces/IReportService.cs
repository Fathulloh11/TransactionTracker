namespace MyExpenseTracker.Services.Interfaces
{
    public interface IReportService
    {
        Task<object> GetMonthlyReportAsync();
    }
}
