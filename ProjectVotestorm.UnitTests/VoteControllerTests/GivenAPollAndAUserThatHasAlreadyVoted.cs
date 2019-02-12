using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Controllers;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.UnitTests.Helpers;

namespace ProjectVotestorm.UnitTests.VoteControllerTests
{
    [TestFixture]
    public class GivenAPollAndAUserThatHasAlreadyVoted
    {
        private readonly string _userIdWhoAlreadyVoted = Guid.NewGuid().ToString();
        private readonly string _mockPollId = Guid.NewGuid().ToString();
        private OkObjectResult _result;

        [OneTimeSetUp]
        public async Task WhenGettingIfTheUserHasVoted()
        {
            // Generates a set of mock votes, then sets their pollId 
            // to the mockPollId. Selects the first one and
            // changes the identity to the user who already voted.
            var mockVotes = FakerHelpers.PluralityVoteFaker.Generate(5);
            mockVotes.ForEach(vote => vote.PollId = _mockPollId);
            mockVotes.First().Identity = _userIdWhoAlreadyVoted;

            var mockPollRepository = new Mock<IPollRepository>();
            var mockVoteRepository = new Mock<IVoteRepository>();
            mockVoteRepository.Setup(mock => mock.Get(It.IsAny<string>())).ReturnsAsync((string pollId) =>
                pollId == mockVotes.First().PollId ? mockVotes : null
            );

            var voteController = new VoteController(mockVoteRepository.Object, mockPollRepository.Object, new NullLogger<VoteController>());
            _result = (OkObjectResult) await voteController.GetHasVoted(_mockPollId, _userIdWhoAlreadyVoted);
        }

        [Test]
        public void ThenTheStatusCodeIs200Ok()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void ThenTheResultShouldBeTrue()
        {
            var hasUserAlreadyVoted = (bool) _result.Value;
            Assert.That(hasUserAlreadyVoted, Is.EqualTo(true));
        }
    }
}
