using System.Linq;
using System.Threading.Tasks;
using Bogus;
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
    public class GivenThatPollsCanBeRetrievedAndSeveralPollsExist
    {
        private OkObjectResult _response;
        private PollResponse _expectedPollResponse;

        [OneTimeSetUp]
        public async Task WhenThePollControllerGetMethodIsInvokedWithAnExistingPollId()
        {
            var mockPollIdGenerator = new Mock<IPollIdGenerator>();
            var mockPollRepository = new Mock<IPollRepository>();

            var mockPolls = FakerHelpers.PollFaker.Generate(5);

            mockPollRepository.Setup(repository => repository.Read(It.IsAny<string>()))
                .ReturnsAsync((string id) => mockPolls.FirstOrDefault(poll => poll.Id == id));
             
            var pollController = new PollController(mockPollIdGenerator.Object, mockPollRepository.Object);

            _expectedPollResponse = new Faker().PickRandom(mockPolls);
            _response = (OkObjectResult) await pollController.GetPoll(_expectedPollResponse.Id);
        }

        [Test]
        public void ThenTheStatusCodeIs200Ok()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void ThenTheCorrectPollIsReturned()
        {
            var pollResponse = (PollResponse) _response.Value;

            Assert.That(pollResponse.Id, Is.EqualTo(_expectedPollResponse.Id));
            Assert.That(pollResponse.Prompt, Is.EqualTo(_expectedPollResponse.Prompt));
            Assert.That(pollResponse.PollType, Is.EqualTo(_expectedPollResponse.PollType));
            Assert.That(pollResponse.Options, Is.EqualTo(_expectedPollResponse.Options));
            Assert.That(pollResponse.IsActive, Is.EqualTo(_expectedPollResponse.IsActive));
            Assert.That(pollResponse.AdminIdentity, Is.EqualTo(_expectedPollResponse.AdminIdentity));
        }
    }
}
