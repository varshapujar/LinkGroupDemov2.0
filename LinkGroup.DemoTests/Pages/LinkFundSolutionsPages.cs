using LinkGroup.DemoTests.Helpers;
using LinkGroup.DemoTests.HooksFile;
using LinkGroup.DemoTests.ReusableFile;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkGroup.DemoTests.Pages
{
    public class LinkFundSolutionsPages
    {
        IWebDriver driver;
        private ReusableMethods reusableMethods = new ReusableMethods();
        public LinkFundSolutionsPages()
        {
            driver = ReusableMethods.driver;
        }
        

        TestData inputDataFile = TestData.getInstance();

        protected IWebElement agreeBtn => driver.FindElement(By.XPath("//a[text()='Agree']"));
        protected IWebElement fundsDrpDwn => driver.FindElement(By.Id("navbarDropdown"));
        protected IWebElement unitedKingdomFundLink => driver.FindElement(By.XPath("//a[contains(text(),'UK')]"));
        protected IWebElement switzerlandFundLink =>  driver.FindElement(By.XPath("//a[contains(text(),'Swiss')]"));
        protected IWebElement fundsForUK => driver.FindElement(By.XPath("//h1[contains(text(),'UK')]"));
        protected IWebElement fundsForSwiss => driver.FindElement(By.XPath("//h1[contains(text(),'Swiss')]"));

        public void launchLinkFundSolutionsUrl(string url)
        {
            reusableMethods.launchUrl(url);
            LogFile.LogInformation("Url is launched successfully");
            reusableMethods.WaitForPageToLoadComplete(10);
        }
        public void clickOnAgreeCookies()
        {
            bool a = reusableMethods.IsElementDisplayed(agreeBtn);
            if (a == true)
            {
                reusableMethods.ClickElement(agreeBtn);
                LogFile.LogInformation("Clicked on Agree button");
            }
            else
            {
                LogFile.LogInformation("Agree button is not displayed");
            }
        }
        public void clickUnitedKingdom()
        {
            reusableMethods.MouseHover(fundsDrpDwn);
            LogFile.LogInformation("Mouse over click on funds drop down");
            reusableMethods.ClickElement(unitedKingdomFundLink);
            LogFile.LogInformation("Clicked on United kingdom fund link");         
        }
        public void clickSwitzerland()
        {
            reusableMethods.MouseHover(fundsDrpDwn);
            LogFile.LogInformation("Mouse over click on funds drop down");
            reusableMethods.ClickElement(switzerlandFundLink);
            LogFile.LogInformation("Clicked on Switzerland fund link");
        }
        public void verifyUKFunds()
        {
            string actualResult = reusableMethods.GetElementText(fundsForUK);
            string expectedResult = inputDataFile.expectedFundForUK;
            try
            {
                Assert.IsTrue(actualResult.Contains(expectedResult));
                LogFile.LogInformation("UK funds are verified successfully.");
            }
            catch (Exception)
            {
                reusableMethods.CaptureScreenshot();
                LogFile.LogErrorInformation("Failed due to expected and actual value differs::" + "Expected value is: " + expectedResult + "Actual value is:" + actualResult);
                SetupFile._status = "Fail";
                SetupFile._statusMessage = "Failed due to expected and actual value differs::" + "Expected value is: " + expectedResult + "Actual value is:" + actualResult;
                throw new Exception("Failed due to expected and actual value differs::" + "Expected value is: " + expectedResult + "Actual value is:" + actualResult);
            }
        }
        public void verifySwissFunds()
        {
            string actualResult = reusableMethods.GetElementText(fundsForSwiss);
            string expectedResult = inputDataFile.expectedFundForSwiss;
            try
            {
                Assert.IsTrue(actualResult.Contains(expectedResult));
                LogFile.LogInformation("Swiss funds are verified successfully.");
            }
            catch (Exception)
            {
                reusableMethods.CaptureScreenshot();
                LogFile.LogErrorInformation("Failed due to expected and actual value differs::" + "Expected value is: " + expectedResult + "Actual value is:" + actualResult);
                SetupFile._status = "Fail";
                SetupFile._statusMessage = "Failed due to expected and actual value differs::" + "Expected value is: " + expectedResult + "Actual value is:" + actualResult;
                throw new Exception("Failed due to expected and actual value differs::" + "Expected value is: " + expectedResult + "Actual value is:" + actualResult);
            }
        }
    }
}
