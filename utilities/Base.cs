

using System;
using System.Configuration;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CSharpSelFramework.utilities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSelFramework.utilities
{
    public class Base
    {
        public required ExtentReports extent;
        public required ExtentTest test;
        private String browser = "null";

        // Prepare Extent Report File
        [OneTimeSetUp]
        public void Setup()

        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "/Report.html";
            var htmlReporter = new  ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
        }


        // Initiate IWebDriver
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [SetUp]
        public void StartBrowser()

        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            // Load Configuration from appsettings.json file
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Set the base directory where the JSON is located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the Environment and Browser from configuration
            String environment = configuration["Environment"];
            String browser = configuration["Browser"];
            String baseUrl = configuration[$"Urls:{environment}"];

            InitBrowser(browser);

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = baseUrl;
        }

        public IWebDriver getDriver()

        {
            return driver.Value;
        }

        public void InitBrowser(string browserName)

        {

            switch (browserName)
            {

                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Edge":

                    driver.Value = new EdgeDriver();
                    break;
            }
        }

        [TearDown]
        public void AfterTest()

        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;



            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {

                // test.Fail("Test failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail,"test case failed.");

            }
            else if (status == TestStatus.Passed)
            {
          
            }

            extent.Flush();
            driver.Value.Quit();
        }

        /*public MediaEntityModelProvider captureScreenShot(IWebDriver driver,String screenShotName)

        {
            ITakesScreenshot ts= (ITakesScreenshot)driver;
           var screenshot= ts.GetScreenshot().AsBase64EncodedString;

           return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();




        }
        */
    }
}
