using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProjectVotestorm.AcceptanceTests.Pages.AngularComponents;

namespace ProjectVotestorm.AcceptanceTests.Pages
{
    public class PollPage
    {
        private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;
        public readonly SubmitVoteComponent SubmitVoteComponent;
        public readonly PollAdminComponent PollAdminComponent;

        public PollPage()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(driver => SubmitVoteComponent.Exists() || PollAdminComponent.Exists());

            if (SubmitVoteComponent.Exists())
                SubmitVoteComponent = new SubmitVoteComponent();

            if (PollAdminComponent.Exists())
                PollAdminComponent = new PollAdminComponent();
        }
    }
}
