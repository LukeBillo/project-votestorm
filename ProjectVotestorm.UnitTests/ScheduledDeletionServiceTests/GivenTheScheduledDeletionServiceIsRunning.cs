using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Data;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.UnitTests.ScheduledDeletionServiceTests
{
    [TestFixture]
    public class GivenTheScheduledDeletionServiceIsRunning
    {
        private Mock<IPollRepository> _mockPollRepository;
        private Mock<ILogger<ScheduledPollDeletion>> _mockLogger;
        private DateTime _deletionTime;

        [OneTimeSetUp]
        public void WhenRunningTheDeletePollsFunction()
        {
            _mockPollRepository = new Mock<IPollRepository>();
            _mockPollRepository.Setup(mock => mock.Delete(It.IsAny<DateTime>())).Returns(Task.CompletedTask);

            _mockLogger = new Mock<ILogger<ScheduledPollDeletion>>();

            var configBuilder = new ConfigurationBuilder();
            configBuilder.Properties.Add("AutoPollDeletion:timerFrequencyHours", 1);
            configBuilder.Properties.Add("AutoPollDeletion:pollMaxAgeDays", 1);
            var mockConfig = configBuilder.Build();

            var scheduledDeletionService = new ScheduledPollDeletion(_mockPollRepository.Object, _mockLogger.Object, mockConfig);
            scheduledDeletionService.DeletePolls(new object());
        }

        [Test]
        public void ThenScheduledDeletionInformationWasLogged()
        {
            _mockLogger.Verify(mock => mock.LogInformation(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void ThenThePollsWereDeleted()
        {
            _deletionTime = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            _mockPollRepository.Verify(mock => mock.Delete(_deletionTime));
        }
    }
}
