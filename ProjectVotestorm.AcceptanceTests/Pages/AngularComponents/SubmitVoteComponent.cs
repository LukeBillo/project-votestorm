using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ProjectVotestorm.AcceptanceTests.Pages.AngularComponents
{
    public class SubmitVoteComponent
    {
        private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;
        private IWebElement Prompt => _webDriver.FindElement(By.CssSelector("submit-vote .prompt"));
        private IReadOnlyCollection<IWebElement> Responses => _webDriver.FindElements(By.CssSelector("submit-vote .response"));
        private IWebElement SubmitButton => _webDriver.FindElement(By.CssSelector("submit-vote button[type=submit]"));
        private IWebElement SuccessMessage => _webDriver.FindElement(By.CssSelector("submit-vote successfully-voted h3"));
        private IWebElement SuccessReturnButton => _webDriver.FindElement(By.CssSelector("submit-vote successfully-voted button"));

        public SubmitVoteComponent()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("submit-vote button[type=submit]")));
        }

        public string PromptText => Prompt.Text;
        public List<string> PollResponses => Responses.Select(response => response.Text).ToList();

        public void SelectResponse(int index)
        {
            var responseRadioButton = _webDriver.FindElement(By.CssSelector($"response-{index} .mat-radio-outer-circle"));
            responseRadioButton.Click();
        }

        public void ClickSubmitButton()
        {
            SubmitButton.Click();
        }

        public string SuccessMessageText => SuccessMessage.Text;

        public HomePage ClickReturnToHomePage()
        {
            SuccessReturnButton.Click();
            return new HomePage();
        }

        public static bool IsVisible()
        {
            return ExpectedConditions.ElementIsVisible(By.CssSelector("submit-vote")) != null;
        }
    }
}
