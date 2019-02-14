using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ProjectVotestorm.AcceptanceTests.Pages.AngularComponents
{
    public class PollAdminComponent
    {
         private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;

         private IWebElement ClosePollButton => _webDriver.FindElement(By.CssSelector("poll-admin button[type=submit]"));
         private IWebElement Prompt => _webDriver.FindElement(By.CssSelector("poll-admin .prompt"));
         private IEnumerable<IWebElement> Options => _webDriver.FindElements(By.CssSelector("poll-admin .options mat-list-item-content"));

         public PollAdminComponent()
         {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(driver => Exists());
         }

         public string PromptText => Prompt.Text;

         public List<string> OptionsText => Options.Select(option => option.Text).ToList();

        public static bool Exists()
        {
            try
            {
                GlobalSetup.WebDriver.FindElement(By.CssSelector("poll-admin"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsPollClosed()
         {
             return !ClosePollButton.Enabled;
         }

         public void ClosePoll()
         {
             ClosePollButton.Click();

             var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
             wait.Until(_ => !ClosePollButton.Enabled);
         }
    }
}