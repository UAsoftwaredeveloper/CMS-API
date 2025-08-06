using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class JobWorkerService : BackgroundService
    {
        private readonly ILogger<JobWorkerService> _logger;
        public JobWorkerService(ILogger<JobWorkerService> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Application Restart Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now.Date;
                var restartTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 1, 0, 0); // 1 AM today

                // If it's past 1 AM, set the restart time for tomorrow
                if (currentTime > restartTime)
                {
                    restartTime = restartTime.AddDays(1);
                }

                var timeToWait = restartTime - currentTime;
                _logger.LogInformation($"Next restart scheduled at: {restartTime}");

                await Task.Delay(timeToWait, stoppingToken); // Wait until 1 AM

                RestartApplication(); // Restart the application
            }
        }

        private void RestartApplication()
        {
            _logger.LogInformation("Restarting application...");

            // You can choose to restart the application using the same process or a different way.
            // Example: Restart using the command line
            Process.Start("dotnet", "run");

            // Optionally, kill the current process to simulate a restart
            Environment.Exit(0);
        }
    }
}
