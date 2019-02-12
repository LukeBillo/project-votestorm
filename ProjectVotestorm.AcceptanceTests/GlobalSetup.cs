using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ProjectVotestorm.AcceptanceTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        public static IWebDriver WebDriver { get; private set; }
        private IWebHost _serverHost;

        [OneTimeSetUp]
        public async Task ServerAndWebDriverSetup()
        {
            _serverHost = Program
                .CreateWebHostBuilder(new string[0])
                .Build();

            await _serverHost.StartAsync();

            var chromeDriverService = ChromeDriverService.CreateDefaultService(Directory.GetCurrentDirectory());
            WebDriver = new ChromeDriver(chromeDriverService);
        }

        [OneTimeTearDown]
        public async Task Cleanup()
        {
            WebDriver.Quit();
            await _serverHost.StopAsync(TimeSpan.FromSeconds(5));
        }
    }
}
