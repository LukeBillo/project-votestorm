using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ProjectVotestorm.AcceptanceTests.Pages.AngularComponents
{
    public class PollAdminComponent
    {
         private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;

         private IWebElement ClosePollButton => _webDriver.FindElement(By.CssSelector("poll-admin button[type=submit]"));
         private IWebElement Prompt => _webDriver.FindElement(By.CssSelector("poll-admin .prompt"));

         private IReadOnlyCollection<IWebElement> Options => _webDriver.FindElements(By.CssSelector("poll-admin .options"));

         public PollAdminComponent()
         {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(driver => IsVisible());
         }

         public string PromptText => Prompt.Text;

         public List<string> OptionsText => Options.Select(option => option.Text).ToList();

         public static bool IsVisible()
         {
             return ExpectedConditions.ElementIsVisible(By.CssSelector("poll-admin")) != null;
         }

         public bool IsPollClosed()
         {
             return ClosePollButton.Enabled;
         }

         public void ClosePoll()
         {
             ClosePollButton.Click();
         }
    }
}