using System.Linq;
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
    public class GivenThatSeveralPollsExist
    {
        private const string NonExistentPollId = "abcde";
        private NotFoundObjectResult _getResponse;
        private NotFoundObjectResult _putResponse;

        [OneTimeSetUp]
        public async Task WhenPollControllerMethodsAreInvokedWithANonExistingPollId()
        {
            var mockPollIdGenerator = new Mock<IPollIdGenerator>();
            var mockPollRepository = new Mock<IPollRepository>();

            var mockPolls = FakerHelpers.PollFaker.Generate(5);

            mockPollRepository.Setup(repository => repository.Read(It.IsAny<string>()))
                .ReturnsAsync((string id) => mockPolls.FirstOrDefault(poll => poll.Id == id));

            var pollController = new PollController(mockPollIdGenerator.Object, mockPollRepository.Object, new NullLogger<PollController>());

            Assert.That(!mockPolls.Exists(poll => poll.Id == NonExistentPollId), "A mock poll was created with an unexpected ID. Try running the tests again.");
            _getResponse = (NotFoundObjectResult) await pollController.GetPoll(NonExistentPollId);
            _putResponse = (NotFoundObjectResult) await pollController.SetPollState(NonExistentPollId, new SetPollStateRequest());
;        }

        [Test]
        public void ThenTheStatusCodeIs404NotFound()
        {
            Assert.That(_getResponse.StatusCode, Is.EqualTo(404));
            Assert.That(_putResponse.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void ThenTheMessageIsCorrect()
        {
            Assert.That(_getResponse.Value, Is.EqualTo("No poll found with the given ID"));
            Assert.That(_putResponse.Value, Is.EqualTo("No poll found with the given ID"));
        }
    }
}
