using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProjectVotestorm.AcceptanceTests.Pages.AngularComponents;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ProjectVotestorm.AcceptanceTests.Pages
{
    public class VotePage
    {
        private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;
        public readonly SubmitVoteComponent SubmitVoteComponent;

        public VotePage()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("submit-vote")));

            SubmitVoteComponent = new SubmitVoteComponent();
        }
    }
}
