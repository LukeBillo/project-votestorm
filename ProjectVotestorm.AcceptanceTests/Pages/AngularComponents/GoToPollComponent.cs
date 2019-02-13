using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ProjectVotestorm.AcceptanceTests.Pages.AngularComponents
{
    public class GoToPollComponent
    {
        private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;

        private IWebElement GoToPollInputField => _webDriver.FindElement(By.CssSelector("goto-poll input"));
        private IWebElement GoButton => _webDriver.FindElement(By.CssSelector("goto-poll button[type=submit]"));

        public GoToPollComponent()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("goto-poll button[type=submit]")));
        }

        public void EnterPollId(string pollId)
        {
            GoToPollInputField.SendKeys(pollId);
        }

        public VotePage ClickGoButton()
        {
            GoButton.Click();
            return new VotePage();
        }
    }
}
