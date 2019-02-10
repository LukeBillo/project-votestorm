using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.Data
{
    public class ScheduledPollDeletion : IHostedService, IDisposable
    {
        private IPollRepository _pollRepository;
        private Timer _timer;
        private ILogger<ScheduledPollDeletion> _logger;

        public ScheduledPollDeletion(IPollRepository pollRepository, ILogger<ScheduledPollDeletion> logger)
        {
            _pollRepository = pollRepository;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DeletePolls, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void DeletePolls(Object state)
        {
            _logger.LogInformation("Starting the scheduled poll deletion task");
            _pollRepository.Delete(DateTime.Now.Subtract(TimeSpan.FromDays(1)));
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