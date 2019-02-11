using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GivenAPollWhichAlreadyHasVotes
    {
        private readonly string _userId = Guid.NewGuid().ToString();
        private string _mockPollId;
        private Mock<IPollRepository> _mockPollRepository;
        private Mock<IVoteRepository> _mockVoteRepository;
        private CreatePluralityVoteRequest _voteRequest;
        private ConflictObjectResult _result;

        [OneTimeSetUp]
        public async Task WhenAUserVotesOnThePoll()
        {
            var mockPoll = FakerHelpers.PollFaker.Generate();
            _mockPollId = mockPoll.Id;
            mockPoll.IsActive = false;

            var mockVotes = FakerHelpers.PluralityVoteFaker.Generate(5);
            mockVotes.ForEach(vote => vote.PollId = _mockPollId);
            mockVotes.First().Identity = _userId;

            _mockPollRepository = new Mock<IPollRepository>();
            _mockPollRepository.Setup(mock => mock.Read(It.IsAny<string>())).ReturnsAsync(
                (string id) => id == mockPoll.Id ? mockPoll : null
            );

            _mockVoteRepository = new Mock<IVoteRepository>();
            _mockVoteRepository.Setup(mock => mock.Get(It.IsAny<string>())).ReturnsAsync(
                (string id) => id == mockVotes.First().PollId ? mockVotes : null
            );
            _mockVoteRepository.Setup(mock => mock.Create(It.IsAny<CreatePluralityVoteRequest>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var voteController = new VoteController(_mockVoteRepository.Object, _mockPollRepository.Object);
            _voteRequest = new CreatePluralityVoteRequest
            {
                Identity = _userId,
                SelectionIndex = 0
            };

            _result = (ConflictObjectResult) await voteController.SubmitVote(_voteRequest, _mockPollId);
        }

        [Test]
        public void ThenTheStatusCodeIs409Conflict()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(409));
        }

        [Test]
        public void ThenTheVotesWereReadToSeeIfTheUserAlreadyVoted()
        {
            _mockVoteRepository.Verify(mock => mock.Get(_mockPollId), Times.Once);
        }

        [Test]
        public void ThenTheVoteWasNotCreatedBecauseTheUserAlreadyVoted()
        {
            _mockVoteRepository.Verify(mock => mock.Create(_voteRequest, _mockPollId), Times.Never);
        }
    }
}
