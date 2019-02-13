using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GivenTheHomePageHasTheGoToPollComponentAndAPollExistsWithAdminUser
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
        public void ThenItShouldGoToThePollPageAndShowThePoll()
        {
            var pollPage = _homePage.GoToPollComponent.ClickGoButton();

            Assert.That(pollPage.SubmitVoteComponent, Is.Null);
            Assert.That(pollPage.PollAdminComponent, Is.Not.Null);

            var adminPage = pollPage.PollAdminComponent;

            Assert.That(adminPage.PromptText, Is.EqualTo(_existingPoll.Prompt));
            Assert.That(adminPage.OptionsText.All(option => _existingPoll.Options.Contains(option)), Is.True);
            Assert.That(adminPage.IsPollClosed, Is.True);
        }
    }
}
