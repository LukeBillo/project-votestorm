using NUnit.Framework;
using ProjectVotestorm.AcceptanceTests.Pages;

namespace ProjectVotestorm.AcceptanceTests.HomeTests
{
    [TestFixture]
    public class GivenTheHomePageContainsSeveralComponents
    {
        private HomePage _homePage;

        [OneTimeSetUp]
        public void WhenGoingToTheHomePage()
        {
            _homePage = new HomePage();
        }

        [Test]
        public void ThenYouCanInteractWithTheNavBarFromTheHomePage()
        {
            // the navbar home button should just reload the home page;
            // fairly pointless, but the navbar should be visible on all
            // pages for consistency.
            _homePage = _homePage.NavBarComponent.ClickHomeButton();
        }

        [Test]
        public void ThenYouCanInteractWithTheCreatePollComponentFromTheHomePage()
        {
            _homePage.CreatePollComponent.EnterPrompt("Checking the prompt input exists!");
        }

        [Test]
        public void ThenYouCanInteractWithTheGoToPollComponentFromTheHomePage()
        {
            _homePage.GoToPollComponent.EnterPollId("Hello");
        }
    }
}
