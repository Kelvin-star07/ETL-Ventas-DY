using VentasETL.Aplication.Interfaces;

namespace VentasETL.Presetacion.WorkerServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                    _logger.LogInformation("Worker running ETL at: {time}", DateTimeOffset.Now);

                try
                {
                   
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IWorkeServiceFinal>();
                        await service.RunETLAsync();
                    }

                    _logger.LogInformation("ETL execution finished successfully at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing ETL at: {time}", DateTimeOffset.Now);
                }

               
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }















    }
}
