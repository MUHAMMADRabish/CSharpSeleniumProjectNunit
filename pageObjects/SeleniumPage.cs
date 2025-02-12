using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.pageObjects
{
    public class SeleniumPage
    {
        private IWebDriver driver;

        //Pageobject factory

        [FindsBy(How = How.XPath, Using = "//a//*[contains(text(),'Documentation')]")]
        private IWebElement linkDocumentation;

        public SeleniumPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public Boolean verifySeleniumPageLoad() {
            Thread.Sleep(8000);

            return linkDocumentation.Displayed;
        }

    }
}
