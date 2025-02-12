
using CSharpSelFramework.pageObjects;
using CSharpSelFramework.utilities;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace LoginTestCase
{
    [Parallelizable(ParallelScope.Self)]
    public class LoginTestCase : Base
    {

        [Test, Category("Smoke")]
        public void VerifyLoginTestFeature()

        {
            // Load Configuration from appsettings.json file
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Set the base directory where the JSON is located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            // Get the Environment and Browser from configuration
            String environment = configuration["Environment"];

            // Get username and password from the correct environment section
            String username = configuration[$"Credentials:{environment}:Username"];
            String password = configuration[$"Credentials:{environment}:Password"];

            LoginPage loginPage = new LoginPage(getDriver());
            Boolean result = loginPage.validLogin(username,password);
            Assert.That(result, Is.True);
        }

    }

}
