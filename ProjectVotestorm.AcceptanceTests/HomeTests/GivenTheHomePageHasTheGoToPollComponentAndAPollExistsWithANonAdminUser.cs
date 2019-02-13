using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProjectVotestorm.AcceptanceTests.Pages;
using ProjectVotestorm.Data.Models.Enums;
using ProjectVotestorm.Data.Models.Http;
using ProjectVotestorm.Data.Repositories;
using ProjectVotestorm.Data.Utils;

namespace ProjectVotestorm.AcceptanceTests.HomeTests
{
    [TestFixture]
    public class GivenTheHomePageHasTheGoToPollComponentAndAPollExistsWithANonAdminUser
    {
        private HomePage _homePage;
        private string _existingPollId;
        private readonly CreatePollRequest _existingPoll = new CreatePollRequest
        {
            Prompt = "I like prompts, do you?",
            Options = new List<string> { "I love prompts", "I hate prompts", "They're okay I guess?", "I'm just undecided" },
            PollType = PollType.Plurality,
            AdminIdentity = Guid.NewGuid().ToString()
        };

        [OneTimeSetUp]
        public async Task WhenOnTheHomePageAndGoingToThePollById()
        {
            var pollIdGenerator = GlobalSetup.ServerHost.Services.GetService<IPollIdGenerator>();
            _existingPollId = pollIdGenerator.Generate();

            var pollRepository = GlobalSetup.ServerHost.Services.GetService<IPollRepository>();
            await pollRepository.Create(_existingPollId, _existingPoll);

            _homePage = new HomePage();
            _homePage.GoToPollComponent.EnterPollId(_existingPollId);
        }

        [Test]
        public void ThenItShouldGoToTheVotePageWithTheCorrectPoll()
        {
            var votePage = _homePage.GoToPollComponent.ClickGoButton();

            Assert.That(votePage.SubmitVoteComponent.PromptText, Is.EqualTo(_existingPoll.Prompt));

            foreach (var option in _existingPoll.Options)
            {
                Assert.That(votePage.SubmitVoteComponent.PollResponses.Contains(option));
            }
        }
    }
}
