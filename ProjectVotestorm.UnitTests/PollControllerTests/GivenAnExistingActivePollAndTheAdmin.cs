using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Controllers;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;
using ProjectVotestorm.UnitTests.Helpers;

namespace ProjectVotestorm.UnitTests.PollControllerTests
{
    [TestFixture]
    public class GivenAnExistingActivePollAndTheAdmin
    {
        private readonly PollResponse _existingPoll = FakerHelpers.PollFaker.Generate();
        private Mock<IPollRepository> _mockPollRepository;
        private CreatePollActivateRequest _activateRequest;
        private OkResult _result;

        [OneTimeSetUp]
        public async Task WhenThePollIsClosedByTheAdmin()
        {
            var mockPollIdGenerator = new Mock<IPollIdGenerator>();
            _mockPollRepository = new Mock<IPollRepository>();

            _mockPollRepository.Setup(mock => mock.Read(It.IsAny<string>()))
                .ReturnsAsync((string id) => id == _existingPoll.Id ? _existingPoll : null);

            _mockPollRepository.Setup(mock => mock.Update(It.IsAny<string>(), It.IsAny<CreatePollActivateRequest>()))
                .Returns(() => Task.CompletedTask);

            _activateRequest = new CreatePollActivateRequest
            {
                IsActive = false,
                AdminIdentity = _existingPoll.AdminIdentity
            };

            var pollController = new PollController(mockPollIdGenerator.Object, _mockPollRepository.Object);
            _result = (OkResult) await pollController.SetPollState(_existingPoll.Id, _activateRequest);
        }

        [Test]
        public void ThenTheStatusCodeIs200Ok()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void ThenThePollWasRead()
        {
            _mockPollRepository.Verify(mock => mock.Read(_existingPoll.Id), Times.Once);
        }

        [Test]
        public void ThenThePollWasUpdated()
        {
            _mockPollRepository.Verify(mock => mock.Update(_existingPoll.Id, _activateRequest));
        }
    }
}
