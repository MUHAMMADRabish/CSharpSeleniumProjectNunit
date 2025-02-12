using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace CSharpSelFramework.pageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        //Pageobject factory

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement signInButton;

        [FindsBy(How = How.XPath, Using = "//div[@id='error-for-username']")]
        private IWebElement verifyErrorMessage;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public Boolean validLogin(string user, string pass) {
            username.SendKeys(user);
            password.SendKeys(pass);
            signInButton.Click();

            return verifyErrorMessage.Displayed;
        }

    }
}
