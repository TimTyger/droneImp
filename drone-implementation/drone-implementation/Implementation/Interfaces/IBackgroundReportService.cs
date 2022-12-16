using System.Threading;

namespace drone_implementation.Implementation.Interfaces
{
    public interface IBackgroundReportService
    {
        Task GenerateReport(CancellationToken stoppingToken);
    }
}
