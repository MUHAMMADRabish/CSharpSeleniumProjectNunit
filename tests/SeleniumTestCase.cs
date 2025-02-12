
using CSharpSelFramework.pageObjects;
using CSharpSelFramework.utilities;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace SelenumTestCase
{
    [Parallelizable(ParallelScope.Self)]
    public class SelenumTestCase : Base
    {

        [Test, Category("Regression")]
        public void VerifySeleniumTestFeature()

        {
            // Load Configuration from appsettings.json file
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Set the base directory where the JSON is located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            // Get the Environment and Browser from configuration
            String environment = configuration["Environment"];

            GoogleSearchPage googlePage = new GoogleSearchPage(getDriver());
            googlePage.setSearchString();

            SeleniumPage seleniumPage = googlePage.clickFirstSearchResult();
            Boolean result = seleniumPage.verifySeleniumPageLoad();

            Assert.That(result, Is.True);
        }

    }

}
