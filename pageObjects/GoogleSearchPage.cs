using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.pageObjects
{
    public class GoogleSearchPage
    {
        private IWebDriver driver;

        //Pageobject factory

        [FindsBy(How = How.XPath, Using = "//textarea[@title='Search']")]
        private IWebElement txtSearchTextBox;

        [FindsBy(How = How.XPath, Using = "(//input[@value='Google Search'])[2]")]
        private IWebElement btnSearch;

        [FindsBy(How = How.XPath, Using = "(//*[contains(text(),'Search Results')]/parent::div//a)[1]")]
        private IWebElement linkFirstSearchResult;

        public GoogleSearchPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public GoogleSearchPage setSearchString() {
            txtSearchTextBox.SendKeys("selenium testing");
            btnSearch.Click();

            return this;
        }

        public SeleniumPage clickFirstSearchResult()
        {
            linkFirstSearchResult.Click();

            return new SeleniumPage(driver);
        }

    }
}
