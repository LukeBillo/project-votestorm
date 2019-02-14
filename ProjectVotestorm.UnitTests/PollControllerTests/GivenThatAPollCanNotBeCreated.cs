using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class GivenThatAPollCanNotBeCreated
    {
        [TestFixture]
        public class GivenThatAPollCanBeCreated
        {
            private const string MockPollId = "abcde";
            private readonly CreatePollRequest _createPollRequest = FakerHelpers.CreatePollRequestFaker.Generate();
            private Mock<IPollRepository> _mockPollRepository;
            private BadRequestObjectResult _result;

            [OneTimeSetUp]
            public async Task WhenCallingTheCreateMethodOnThePollController()
            {
                var mockPollIdGenerator = new Mock<IPollIdGenerator>();
                mockPollIdGenerator.Setup(mock => mock.Generate()).Returns(MockPollId);

                _mockPollRepository = new Mock<IPollRepository>();
                _mockPollRepository.Setup(mock => mock.Create(It.IsAny<string>(), It.IsAny<CreatePollRequest>()))
                    .Throws<InvalidOperationException>();

                var pollController = new PollController(mockPollIdGenerator.Object, _mockPollRepository.Object,
                    new NullLogger<PollController>());

                _result = (BadRequestObjectResult) await pollController.CreatePoll(_createPollRequest);
            }

            [Test]
            public void ThenTheStatusCodeIs400BadRequest()
            {
                Assert.That(_result.StatusCode, Is.EqualTo(400));
            }

            [Test]
            public void ThenTheMessageIsCorrect()
            {
                Assert.That(_result.Value, Is.EqualTo("Invalid poll object"));
            }

            [Test]
            public void ThenThePollRepositoryCreateWasCalled()
            {
                _mockPollRepository.Verify(mock => mock.Create(MockPollId, _createPollRequest), Times.Once);
            }
        }
    }
}
