using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Controllers;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;
using ProjectVotestorm.UnitTests.Helpers;

namespace ProjectVotestorm.UnitTests.PollControllerTests
{
    public class GivenAnExistingActivePollAndAUser
    {
        private readonly PollResponse _existingPoll = FakerHelpers.PollFaker.Generate();
        private Mock<IPollRepository> _mockPollRepository;
        private SetPollStateRequest _activateRequest;
        private UnauthorizedResult _result;

        [OneTimeSetUp]
        public async Task WhenTheUserAttemptsToCloseThePoll()
        {
            var mockPollIdGenerator = new Mock<IPollIdGenerator>();
            _mockPollRepository = new Mock<IPollRepository>();

            _mockPollRepository.Setup(mock => mock.Read(It.IsAny<string>()))
                .ReturnsAsync((string id) => id == _existingPoll.Id ? _existingPoll : null);

            _mockPollRepository.Setup(mock => mock.Update(It.IsAny<string>(), It.IsAny<SetPollStateRequest>()))
                .Returns(() => Task.CompletedTask);

            _activateRequest = new SetPollStateRequest
            {
                IsActive = false,
                AdminIdentity = Guid.NewGuid().ToString()
            };

            var pollController = new PollController(mockPollIdGenerator.Object, _mockPollRepository.Object, new NullLogger<PollController>());
            _result = (UnauthorizedResult) await pollController.SetPollState(_existingPoll.Id, _activateRequest);
        }

        [Test]
        public void ThenTheResponseIsUnauthorized()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public void ThenThePollWasRead()
        {
            _mockPollRepository.Verify(mock => mock.Read(_existingPoll.Id), Times.Once);
        }

        [Test]
        public void ThenThePollWasNotUpdated()
        {
            _mockPollRepository.Verify(mock => mock.Update(_existingPoll.Id, _activateRequest), Times.Never);
        }
    }
}
