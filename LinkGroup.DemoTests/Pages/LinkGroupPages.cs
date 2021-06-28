using LinkGroup.DemoTests.Helpers;
using LinkGroup.DemoTests.HooksFile;
using LinkGroup.DemoTests.ReusableFile;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LinkGroup.DemoTests.Pages
{
    public class LinkGroupPages
    {
        IWebDriver driver;
        private ReusableMethods reusableMethods = new ReusableMethods();
        public LinkGroupPages()
        {
            driver = ReusableMethods.driver;
        }
        TestData inputDataFile = TestData.getInstance();


        protected IWebElement agreeBtn => driver.FindElement(By.XPath("//a[text()='Agree']"));
        protected IWebElement homePageHeading => driver.FindElement(By.Id("las-logo"));
        protected IWebElement searchDrpDwn => driver.FindElement(By.Id("TN-search"));
        protected IWebElement searchTxtBx => driver.FindElement(By.Name("searchTerm"));
        protected IWebElement searchBtn => driver.FindElement(By.XPath("//button[text()='Search']"));
        protected IWebElement searchResults => driver.FindElement(By.XPath("//h4[contains(text(),'Leeds')]"));


        public void launchLinkGroupUrl(string url)
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

        public bool verifyHomePage()
        {     
           bool eleemnt =  reusableMethods.IsElementDisplayed(homePageHeading);
           return eleemnt;
        }
        public void searchForLeeds(String searchValue)
        {
            reusableMethods.MouseHover(searchDrpDwn);
            LogFile.LogInformation("Clicked on search drop down");
            searchTxtBx.SendKeys(searchValue);
            LogFile.LogInformation("Search value is entered");
            Thread.Sleep(2000);
            searchTxtBx.SendKeys(Keys.Enter);
            Thread.Sleep(4000);
        }
        public void verifySearchResults()
        {
            string actualResult = reusableMethods.GetElementText(searchResults);
            string expectedResult = inputDataFile.expectedSearchResults;
            try
            {
                Assert.IsTrue(actualResult.Contains(expectedResult));
                LogFile.LogInformation("Search results are verified successfully.");
            }
            catch(Exception)
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
