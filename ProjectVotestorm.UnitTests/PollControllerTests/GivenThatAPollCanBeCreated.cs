using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectVotestorm.Controllers;
using ProjectVotestorm.Data.Models.Enums;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;

namespace ProjectVotestorm.UnitTests.PollControllerTests
{
    [TestFixture]
    public class GivenThatAPollCanBeCreated
    {
        private static readonly Faker<CreatePollRequest> CreatePollRequestFaker = new Faker<CreatePollRequest>()
            .RuleFor(request => request.Prompt, f => f.Random.Words() + "?")
            .RuleFor(request => request.PollType, _ => PollType.Plurality)
            .RuleFor(request => request.Options, f => f.Random.WordsArray(2, 5).ToList())
            .RuleFor(request => request.AdminIdentity, f => f.Random.Guid().ToString());

        private const string MockPollId = "abcde";
        private readonly CreatePollRequest _createPollRequest = CreatePollRequestFaker.Generate();
        private Mock<IPollRepository> _mockPollRepository;
        private string _expectedLocation;
        private CreatedResult _result;

        [OneTimeSetUp]
        public async Task WhenCallingTheCreateMethodOnThePollController()
        {
            var mockPollIdGenerator = new Mock<IPollIdGenerator>();
            mockPollIdGenerator.Setup(mock => mock.Generate()).Returns(MockPollId);

            _mockPollRepository = new Mock<IPollRepository>();
            _mockPollRepository.Setup(mock => mock.Create(It.IsAny<string>(), It.IsAny<CreatePollRequest>()))
                .Returns(Task.CompletedTask);

            var mockControllerContext = Mock.Of<ControllerContext>();
            mockControllerContext.HttpContext = new DefaultHttpContext();
            mockControllerContext.HttpContext.Request.Scheme = "https";
            mockControllerContext.HttpContext.Request.Host = new HostString("localhost:44301");
            mockControllerContext.HttpContext.Request.Path = "/api/poll";
            _expectedLocation = $"https://localhost:44301/api/poll/{MockPollId}";

            var pollController = new PollController(mockPollIdGenerator.Object, _mockPollRepository.Object)
            {
                ControllerContext = mockControllerContext
            };

            _result = (CreatedResult) await pollController.CreatePoll(_createPollRequest);
        }

        [Test]
        public void ThenTheStatusCodeIs201Created()
        {
            Assert.That(_result.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public void ThenTheLocationOfTheCreatedPollIsCorrect()
        {
            Assert.That(_result.Location, Is.EqualTo(_expectedLocation));
        }

        [Test]
        public void ThenThePollRepositoryCreateWasCalled()
        {
            _mockPollRepository.Verify(mock => mock.Create(MockPollId, _createPollRequest), Times.Once);
        }
    }
}
