using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProjectVotestorm.AcceptanceTests.Pages.AngularComponents;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ProjectVotestorm.AcceptanceTests.Pages
{
    public class HomePage
    {
        private const string PageUrl = GlobalSetup.BaseUrl;
        private readonly IWebDriver _webDriver = GlobalSetup.WebDriver;

        public readonly NavBarComponent NavBarComponent;
        public readonly GoToPollComponent GoToPollComponent;
        public readonly CreatePollComponent CreatePollComponent;

        public HomePage()
        {
            if (_webDriver.Url != PageUrl)
                _webDriver.Navigate().GoToUrl(PageUrl);

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("app-home")));

            NavBarComponent = new NavBarComponent();
            GoToPollComponent = new GoToPollComponent();
            CreatePollComponent = new CreatePollComponent();
        }
    }
}
