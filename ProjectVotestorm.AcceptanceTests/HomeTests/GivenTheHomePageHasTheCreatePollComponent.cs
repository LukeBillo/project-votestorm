using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using ProjectVotestorm.AcceptanceTests.Pages;
using ProjectVotestorm.Data.Models.Enums;
using ProjectVotestorm.Data.Repositories;

namespace ProjectVotestorm.AcceptanceTests.HomeTests
{
    [TestFixture]
    public class GivenTheHomePageHasTheCreatePollComponent
    {
        private HomePage _homePage;
        private const string PollPrompt = "Is the Earth flat?";
        private const string FirstResponse = "It's flat and we're gonna fall off";
        private const string SecondResponse = "It's round, obviously";

        [OneTimeSetUp]
        public void WhenGoingToTheHomePageAndCreatingAPoll()
        {
            _homePage = new HomePage();

            var pollCreator = _homePage.CreatePollComponent;
            pollCreator.EnterPrompt(PollPrompt);
            pollCreator.EnterResponse(0, FirstResponse);
            pollCreator.EnterResponse(1, SecondResponse);
            pollCreator.ClickSubmit();
        }

        [Test]
        public void ThenASuccessfullyCreatedMessageShouldBeShown()
        {
            Assert.That(_homePage.CreatePollComponent.GetCreatedPollMessage().Contains("Your poll has been created"));
        }

        [Test]
        public void ThenTheSuccessMessageShouldContainThePollUrl()
        {
            var pollId = new string(_homePage.CreatePollComponent.GetCreatedPollMessage().TakeLast(5).ToArray());
            Assert.That(Regex.IsMatch(pollId, "[A-Za-z1-9]{5}"));
        }

        [Test]
        public async Task ThenThePollWasCreated()
        {
            var pollId = new string(_homePage.CreatePollComponent.GetCreatedPollMessage().TakeLast(5).ToArray());
            var pollRepository = GlobalSetup.ServerHost.Services.GetService<IPollRepository>();

            var chromeDriver = (ChromeDriver) GlobalSetup.WebDriver;
            var adminIdentity = chromeDriver.WebStorage.LocalStorage.GetItem("identity");

            var createdPoll = await pollRepository.Read(pollId);
            Assert.That(createdPoll.Prompt, Is.EqualTo(PollPrompt));
            Assert.That(createdPoll.Options.ElementAt(0), Is.EqualTo(FirstResponse));
            Assert.That(createdPoll.Options.ElementAt(1), Is.EqualTo(SecondResponse));
            Assert.That(createdPoll.PollType, Is.EqualTo(PollType.Plurality));
            Assert.That(createdPoll.AdminIdentity, Is.EqualTo(adminIdentity));
            Assert.That(createdPoll.IsActive);
        }
    }
}
