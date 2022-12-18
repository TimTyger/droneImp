using drone_Domain.Dtos;
using drone_Domain.Entities;
using drone_implementation.Implementation.Interfaces;
using NLog;
using System.Text;

namespace drone_implementation.Implementation.Services
{
    public class ReportGenerationService : IBackgroundReportService
    {
        private readonly IDroneService _droneService;
        private readonly ILogger<ReportGenerationService> _logger;

        private readonly IConfiguration _config;
        public ReportGenerationService(IDroneService droneService, ILogger<ReportGenerationService> logger,IConfiguration config)
        {
            _droneService = droneService;
            _config = config;
            _logger = logger;
        }
        public async Task GenerateReport(CancellationToken stoppingToken, string time)
        {

            try
            {
                StringBuilder reportBuilder = new StringBuilder();

                if (!stoppingToken.IsCancellationRequested)
                {
                    var drones = await _droneService.GetAll();
                    //.ToListAsync();
                    reportBuilder.AppendLine("DroneId| \t DroneSerialNo| \t BatteryLevel| \t  Time|");
                    if (drones.Data != null)
                    {
                        var report = ((List<Drone>)drones.Data).Select(a => new
                        {
                            droneId = a.Id,
                            droneSerialNo = a.SerialNumber,
                            batteryLevel = a.BatteryLevel,
                            time = time
                        });
                        foreach (var item in report)
                        {
                            reportBuilder.AppendLine($"{item.droneId}| \t {item.droneSerialNo}| \t {item.batteryLevel}| \t  {item.time}|");
                        }
                    }
                    else _logger.LogError($"No Drone Available At this Time {time:f}");

                }
                _logger.LogInformation(reportBuilder.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error({ex.Message}) Occured While Generating Report {time:f}");

            }
        }
    }
}

