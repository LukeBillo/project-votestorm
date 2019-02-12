using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Data;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.UnitTests.Helpers;

namespace ProjectVotestorm.UnitTests.ScheduledDeletionServiceTests
{
    [TestFixture]
    public class GivenTheScheduledDeletionServiceIsRunning
    {
        private Mock<IPollRepository> _mockPollRepository;
        private MockLogger<ScheduledPollDeletion> _mockLogger;
        private DateTime _expectedDeletionTime;

        [OneTimeSetUp]
        public void WhenRunningTheDeletePollsFunction()
        {
            _mockPollRepository = new Mock<IPollRepository>();
            _mockPollRepository.Setup(mock => mock.Delete(It.IsAny<DateTime>())).Returns(Task.CompletedTask);

            _mockLogger = new MockLogger<ScheduledPollDeletion>();

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(mock => mock["AutoPollDeletion:timerFrequencyHours"]).Returns("1");
            mockConfig.Setup(mock => mock["AutoPollDeletion:pollMaxAgeDays"]).Returns("1");
            _expectedDeletionTime = DateTime.Now.Subtract(TimeSpan.FromDays(1));

            var scheduledDeletionService = new ScheduledPollDeletion(_mockPollRepository.Object, _mockLogger, mockConfig.Object);
            scheduledDeletionService.DeletePolls(new object());
        }

        [Test]
        public void ThenInformationWasLoggedAboutTheScheduledDeletion()
        {
            Assert.That(_mockLogger.Logs.Count, Is.EqualTo(4));
            Assert.That(_mockLogger.Logs.Contains("Timer frequency has been set to 1 hours"));
            Assert.That(_mockLogger.Logs.Contains("Poll max age has been set to 1 days"));
            Assert.That(_mockLogger.Logs.Contains("Starting the scheduled poll deletion task"));
            Assert.That(_mockLogger.Logs.Contains("Completed the scheduled poll deletion task"));
        }

        [Test]
        public void ThenThePollsWereDeleted()
        {
            _mockPollRepository.Verify(mock => mock.Delete(It.IsInRange(_expectedDeletionTime, DateTime.Now, Range.Inclusive)));
        }
    }
}
