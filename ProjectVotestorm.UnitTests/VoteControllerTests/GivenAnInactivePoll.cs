using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Controllers;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.UnitTests.Helpers;

namespace ProjectVotestorm.UnitTests.VoteControllerTests
{
    [TestFixture]
    public class GivenAnInactivePoll
    {
        private readonly string _userId = Guid.NewGuid().ToString();
        private string _mockPollId;
        private Mock<IPollRepository> _mockPollRepository;
        private Mock<IVoteRepository> _mockVoteRepository;
        private CreatePluralityVoteRequest _voteRequest;
        private StatusCodeResult _result;

        [OneTimeSetUp]
        public async Task WhenAUserVotesOnThePoll()
        {
            var mockPoll = FakerHelpers.PollFaker.Generate();
            _mockPollId = mockPoll.Id;
            mockPoll.IsActive = false;

            _mockPollRepository = new Mock<IPollRepository>();
            _mockPollRepository.Setup(mock => mock.Read(It.IsAny<string>())).ReturnsAsync(
                (string id) => id == mockPoll.Id ? mockPoll : null
            );

            _mockVoteRepository = new Mock<IVoteRepository>();
            _mockVoteRepository.Setup(mock => mock.Get(It.IsAny<string>())).ReturnsAsync(new List<PluralityVote>());
            _mockVoteRepository.Setup(mock => mock.Create(It.IsAny<CreatePluralityVoteRequest>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var voteController = new VoteController(_mockVoteRepository.Object, _mockPollRepository.Object);
            _voteRequest = new CreatePluralityVoteRequest
            {
                Identity = _userId,
                SelectionIndex = 0
            };

            _result = (StatusCodeResult) await voteController.SubmitVote(_voteRequest, _mockPollId);
        }

        [Test]
        public void ThenTheStatusCodeIs423Locked()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(423));
        }

        [Test]
        public void ThenTheVotesAreReadToCheckIfTheUserAlreadyVoted()
        {
            _mockVoteRepository.Verify(mock => mock.Get(_mockPollId), Times.Once);
        }

        [Test]
        public void ThenThePollIsReadToCheckIfThePollIsActive()
        {
            _mockPollRepository.Verify(mock => mock.Read(_mockPollId), Times.Once);
        }

        [Test]
        public void ThenTheVoteIsNotCreatedBecauseThePollIsInactive()
        {
            _mockVoteRepository.Verify(mock => mock.Create(_voteRequest, _mockPollId), Times.Never);
        }
    }
}
