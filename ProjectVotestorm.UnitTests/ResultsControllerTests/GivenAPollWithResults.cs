using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Controllers;
using ProjectVotestorm.Data.Models.Database;
using ProjectVotestorm.Data.Models.Enums;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.UnitTests.Helpers;

namespace ProjectVotestorm.UnitTests.ResultsControllerTests
{
    [TestFixture]
    public class GivenAPollWithResults
    {
        private Mock<IPollRepository> _mockPollRepository;
        private Mock<IVoteRepository> _mockVoteRepository;
        private List<PluralityVote> _mockVotes;
        private string _mockPollId;
        private PollResponse _mockPoll;
        private OkObjectResult _result;

        [OneTimeSetUp]
        public async Task WhenThePollResultsGetMethodIsInvoked()
        {
            _mockPoll = FakerHelpers.PollFaker.Generate();
            _mockPollId = _mockPoll.Id;

            _mockVotes = FakerHelpers.PluralityVoteFaker.GenerateBetween(5, 10);

            foreach (var vote in _mockVotes)
            {
                if (vote.SelectionIndex >= _mockPoll.Options.Count - 1)
                {
                    vote.SelectionIndex = 0;
                }    
            }

            _mockPollRepository = new Mock<IPollRepository>();
            _mockPollRepository.Setup(mock => mock.Read(It.IsAny<string>())).ReturnsAsync(
                (string id) => id == _mockPoll.Id ? _mockPoll : null
            );

            _mockVoteRepository = new Mock<IVoteRepository>();
            _mockVoteRepository.Setup(mock => mock.Get(It.IsAny<string>())).ReturnsAsync(_mockVotes);
            _mockVoteRepository.Setup(mock => mock.Create(It.IsAny<CreatePluralityVoteRequest>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var voteController = new ResultsController(_mockPollRepository.Object, _mockVoteRepository.Object);
            _result = (OkObjectResult) await voteController.GetResults(_mockPollId, _mockPoll.AdminIdentity);
        }

        [Test]
        public void ThenTheStatusCodeIs200Ok()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void ThenTheResultsAreCorrect()
        {
            var results = (ResultsResponse) _result.Value;

            Assert.That(results.PollType, Is.EqualTo(PollType.Plurality));
            Assert.That(results.TotalVotes, Is.EqualTo(_mockVotes.Count));

            foreach (var option in _mockPoll.Options)
            {
                var optionResult = results.OptionResults.FirstOrDefault(result => result.OptionText == option);

                // no results if there were no votes
                // for this option.
                if (optionResult == null)
                    continue;

                var expectedNumberOfVotes = _mockVotes.Count(vote => vote.SelectionIndex == _mockPoll.Options.IndexOf(option));
                Assert.That(optionResult.NumberOfVotes, Is.EqualTo(expectedNumberOfVotes));
            }
        }
    }
}
