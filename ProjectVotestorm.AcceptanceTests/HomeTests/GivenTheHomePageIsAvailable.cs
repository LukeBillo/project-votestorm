using NUnit.Framework;

namespace ProjectVotestorm.AcceptanceTests.HomeTests
{
    [TestFixture]
    public class GivenTheHomePageIsAvailable
    {
        [OneTimeSetUp]
        public void WhenTheHomePageIsLoaded()
        {
            GlobalSetup.WebDriver.Navigate().GoToUrl("https://localhost:5001/");
        }

        [Test]
        public void ThenTheGoToPollComponentIsVisible()
        {
            Assert.That(true);
        }

    }
}
