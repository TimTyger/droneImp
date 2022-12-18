using ddrone_DataAccess;
using drone_Domain.Dtos;
using drone_implementation.Implementation.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace drone_implementation.Implementation.Services
{
    public class BackgroundReportService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private int _timeInterval;
        public BackgroundReportService(
        IServiceProvider serviceProvider,
        IConfiguration config)
        {
            _serviceProvider = serviceProvider;   
            _config = config;
            _timeInterval = int.Parse(_config.GetSection("ReportIntervalInMinutes").Value);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await GenerateReport(stoppingToken);
        }

        private async Task GenerateReport(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IBackgroundReportService scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<IBackgroundReportService>();

                    await scopedProcessingService.GenerateReport(stoppingToken, DateTime.Now.ToString("f")).ConfigureAwait(false);
                }
                await Task.Delay(_timeInterval * 60000, stoppingToken);
            }

        }
    }
}
