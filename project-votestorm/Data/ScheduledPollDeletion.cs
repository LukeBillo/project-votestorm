using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.Data
{
    public class ScheduledPollDeletion : IHostedService, IDisposable
    {
        private readonly IPollRepository _pollRepository;
        private Timer _timer;
        private readonly ILogger<ScheduledPollDeletion> _logger;
        private readonly double _timerPeriod;
        private readonly double _pollMaxAge;

        public ScheduledPollDeletion(IPollRepository pollRepository, ILogger<ScheduledPollDeletion> logger,
            IConfiguration config)
        {
            _pollRepository = pollRepository;
            _logger = logger;

            _timerPeriod = Convert.ToDouble(config["AutoPollDeletion:timerFrequencyHours"]);
            _pollMaxAge = Convert.ToDouble(config["AutoPollDeletion:pollMaxAgeDays"]);
            
            _logger.LogInformation($"Timer frequency has been set to {_timerPeriod} hours");
            _logger.LogInformation($"Poll max age has been set to {_pollMaxAge} days");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DeletePolls, null, TimeSpan.Zero, TimeSpan.FromHours(_timerPeriod));

            return Task.CompletedTask;
        }

        public void DeletePolls(object state)
        {
            _logger.LogInformation("Starting the scheduled poll deletion task");
            _pollRepository.Delete(DateTime.Now.Subtract(TimeSpan.FromDays(_pollMaxAge)));
            _logger.LogInformation("Completed the scheduled poll deletion task");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}