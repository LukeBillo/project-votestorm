using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ProjectVotestorm.AcceptanceTests.Pages.AngularComponents
{
    public class CreatePollComponent
    {
        private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;

        private IWebElement PromptInput => _webDriver.FindElement(By.CssSelector("create-poll input[formControlName=prompt]"));
        private IReadOnlyCollection<IWebElement> ResponseInputs => _webDriver.FindElements(By.CssSelector("create-poll .response input"));
        private IWebElement AddResponseButton => _webDriver.FindElement(By.CssSelector("create-poll button[type=button]"));
        private IWebElement SubmitButton => _webDriver.FindElement(By.CssSelector("create-poll button[type=submit]"));
        private IWebElement CreatedPollMessage => _webDriver.FindElement(By.CssSelector("create-poll h3"));

        public CreatePollComponent()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("create-poll button[type=submit]")));
        }

        public void EnterPrompt(string prompt)
        {
            PromptInput.SendKeys(prompt);
        }

        public void EnterResponse(int responseIndex, string response)
        {
            var responseInput = ResponseInputs.ElementAt(responseIndex);
            responseInput.SendKeys(response);
        }

        public void AddResponse()
        {
            AddResponseButton.Click();
        }

        public void ClickSubmit()
        {
            SubmitButton.Click();

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("create-poll h3")));
        }

        public string GetCreatedPollMessage()
        {
            return CreatedPollMessage.Text;
        }
    }
}
