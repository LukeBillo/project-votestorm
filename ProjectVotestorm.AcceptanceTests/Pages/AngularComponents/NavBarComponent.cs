using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ProjectVotestorm.AcceptanceTests.Pages.AngularComponents
{
    public class NavBarComponent
    {
        private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;
        private IWebElement HomeButton => _webDriver.FindElement(By.CssSelector("nav-bar button"));

        public NavBarComponent()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("nav-bar button")));
        }

        public HomePage ClickHomeButton()
        {
            HomeButton.Click();
            return new HomePage();
        }
    }
}
