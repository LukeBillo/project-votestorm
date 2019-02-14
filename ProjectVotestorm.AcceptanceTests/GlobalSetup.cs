using System;
using System.IO;
using System.Linq;
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
        public static IWebHost ServerHost { get; private set; }

        public const string BaseUrl = "http://localhost:44300";

        private string GetProjectVotestormDirectory(DirectoryInfo directoryToCheck)
        {
            // Recursively goes upwards (to parent directories) until it finds the project-votestorm folder.
            // This is to set the content root for the server so that ClientApp (front-end)
            // SPA files can be found properly on server startup.
            var projectVotestormDirectory = directoryToCheck.GetDirectories().FirstOrDefault(directory => directory.Name == "project-votestorm");
            return projectVotestormDirectory != null ? projectVotestormDirectory.FullName : GetProjectVotestormDirectory(directoryToCheck.Parent);
        }

        [OneTimeSetUp]
        public async Task ServerAndWebDriverSetup()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            ServerHost = Program
                .CreateWebHostBuilder(new string[0])
                .UseContentRoot(GetProjectVotestormDirectory(currentDirectory))
                .UseUrls("http://localhost:44300", "https://localhost:44301")
                .Build();

            await ServerHost.StartAsync();

            var chromeDriverService = ChromeDriverService.CreateDefaultService(Directory.GetCurrentDirectory());
            WebDriver = new ChromeDriver(chromeDriverService);
            WebDriver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public async Task Cleanup()
        {
            WebDriver.Quit();
            WebDriver.Dispose();

            await ServerHost.StopAsync(TimeSpan.FromSeconds(5));
            ServerHost.Dispose();
        }
    }
}
