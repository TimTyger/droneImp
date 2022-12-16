using drone_Domain.Dtos;
using drone_Domain.Entities;
using drone_implementation.Implementation.Interfaces;

namespace drone_implementation.Implementation.Services
{
    public class ReportGenerationService : IBackgroundReportService
    {
        private readonly IDroneService _droneService;
        private readonly ILogger<ReportGenerationService> _logger;

        private readonly IConfiguration _config;
        private int _timeInterval;
        public ReportGenerationService(IDroneService droneService,IConfiguration config, ILogger<ReportGenerationService> logger)
        {
            _droneService = droneService;
            _logger = logger;
            _config = config;
            _timeInterval = int.Parse(_config.GetSection("ReportIntervalInMinutes").Value);
        }
        public async Task GenerateReport(CancellationToken stoppingToken)
        {
            var time = DateTime.Now;
            while (!stoppingToken.IsCancellationRequested & ((time.Minute + 1) % _timeInterval == 0))
            {
                try
                {
                    var drones = await _droneService.GetAll();
                         //.ToListAsync();

                        if (drones.Data != null)
                        {
                            var report = ((List<Drone>)drones.Data).Select(a => new
                            {
                                droneId = a.Id,
                                droneSerialNo = a.SerialNumber,
                                batteryLevel = a.BatteryLevel,
                                time = time.ToString("f")
                            });
                            foreach (var item in report)
                            {
                                _logger.LogInformation($"{item.droneId}| \t {item.droneSerialNo}| \t {item.batteryLevel}| \t  {item.time}|");
                            }
                        }
                    _logger.LogError($"No Drone Available At this Time {time:f}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error({ex.Message}) Occured While Generating Report {time:f}");

                }
            }
        }
    }
}
